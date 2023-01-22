using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq.Expressions;
using Typely.Core;
using Typely.Core.Builders;
using Typely.Generators.Extensions;

namespace Typely.Generators.Typely.Parsing;

/// <summary>
/// Parse a <see cref="ClassDeclarationSyntax"/> and generates a list of <see cref="EmittableType"/>.
/// </summary>
internal sealed class Parser
{
    private readonly CancellationToken _cancellationToken;
    private readonly Compilation _compilation;
    private readonly Action<Diagnostic> _reportDiagnostic;

    public Parser(Compilation compilation, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
    {
        _compilation = compilation;
        _cancellationToken = cancellationToken;
        _reportDiagnostic = reportDiagnostic;
    }

    /// <summary>
    /// Filter classes having an interface name <see cref="ITypelyConfiguration"/>.
    /// </summary>
    internal static bool IsTypelyConfigurationClass(SyntaxNode syntaxNode) =>
        syntaxNode is ClassDeclarationSyntax c && c.HasInterface(nameof(ITypelyConfiguration));

    internal static bool IsConfigureMethod(SyntaxNode syntaxNode) =>
        syntaxNode is MethodDeclarationSyntax c && c.Identifier.Text == nameof(ITypelyConfiguration.Configure);

    /// <summary>
    /// Filter classes having an interface <see cref="ITypelyConfiguration"/> that matches the 
    /// namespace and returns the <see cref="ClassDeclarationSyntax"/>.
    /// </summary>
    internal static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;
        var classSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax)!;

        return classSymbol.AllInterfaces.Any(x => x.ToString() == typeof(ITypelyConfiguration).FullName)
            ? classDeclarationSyntax
            : null;
    }

    /// <summary>
    /// Execute the different <see cref="ITypelyConfiguration"/> classes founds and generate models of the desired user types.
    /// </summary>
    /// <param name="classes">Classes to parse.</param>
    /// <returns>A list of representation of desired user types.</returns>
    public IReadOnlyList<EmittableType> GetEmittableTypes(IEnumerable<ClassDeclarationSyntax> classes)
    {
        // We enumerate by syntax tree, to minimize impact on performance
        return classes.GroupBy(x => x.SyntaxTree).SelectMany(x => GetEmittableTypes(x.Key)).ToList().AsReadOnly();
    }

    /// <summary>
    /// Execute the different <see cref="ITypelyConfiguration"/> classes and generate models of the desired user types.
    /// </summary>
    /// <param name="syntaxTree">SyntaxTree to parse</param>
    /// <returns>A list of representation of desired user types.</returns>
    private IEnumerable<EmittableType> GetEmittableTypes(SyntaxTree syntaxTree)
    {
        _cancellationToken.ThrowIfCancellationRequested();

        var emittableTypes = new List<EmittableType>();
        var root = syntaxTree.GetRoot();
        var classSyntaxes = root.DescendantNodes().Where(IsTypelyConfigurationClass).ToList();
        foreach (var classSyntax in classSyntaxes)
        {
            var methodSyntax = classSyntax.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .FirstOrDefault(IsConfigureMethod);

            if (methodSyntax == null || methodSyntax.Body == null)
            {
                continue;
            }

            //Phase 1 : Parse C# and get the operations
            var invocationResults = new List<InvocationResult>();
            var invocationResultVariables = new Dictionary<string, InvocationResult>();
            var bodySyntaxNodes = methodSyntax.Body.DescendantNodes();
            foreach (var bodySyntaxNode in bodySyntaxNodes)
            {
                if (bodySyntaxNode is ExpressionStatementSyntax expressionStatementSyntax)
                {
                    var invocationResult = new InvocationResult();
                    invocationResults.Add(invocationResult);
                    ParseInvocationExpression(expressionStatementSyntax.Expression, invocationResult);
                }
                else if (bodySyntaxNode is LocalDeclarationStatementSyntax localDeclarationStatementSyntax)
                {
                    var variable = localDeclarationStatementSyntax.Declaration.Variables.FirstOrDefault();
                    if (variable == null)
                    {
                        throw new NotSupportedException("Local declaration without variable");
                    }

                    var invocationResult = new InvocationResult();
                    invocationResultVariables.Add(variable.Identifier.Text, invocationResult);
                    if (variable.Initializer == null)
                    {
                        throw new NotSupportedException("Initializer null for LocalDeclarationStatementSyntax");
                    }

                    ParseInvocationExpression(variable.Initializer.Value, invocationResult);
                }
            }

            //Phase 2 : Convert all variable syntaxes to flattened declaration syntaxes
            var typelyBuilderParameterName = methodSyntax.ParameterList.Parameters.First().Identifier.Text;
            invocationResults = invocationResults.Where(x => x.Root == typelyBuilderParameterName).ToList();

            //Phase 3 : Create EmittableTypes
            var defaultNamespace = GetNamespace(classSyntax);

            foreach (var invocationResult in invocationResults)
            {
                var invocationEmittableTypes = InvocationResultParserFactory.Create(defaultNamespace, invocationResult).Parse();
                emittableTypes.AddRange(invocationEmittableTypes);
            }
        }

        return emittableTypes;
    }

    private string GetNamespace(SyntaxNode classSyntax)
    {
        while (classSyntax.Parent != null)
        {
            if (classSyntax.Parent is NamespaceDeclarationSyntax namespaceDeclarationSyntax)
            {
                return namespaceDeclarationSyntax.Name.ToString();
            }
            else if (classSyntax.Parent is FileScopedNamespaceDeclarationSyntax fileScopedNamespaceDeclarationSyntax)
            {
                return fileScopedNamespaceDeclarationSyntax.Name.ToString();
            }

            classSyntax = classSyntax.Parent;
        }
        return string.Empty;
    }

    public void ParseInvocationExpression(CSharpSyntaxNode syntaxNode, InvocationResult syntaxInvocationResult)
    {
        if (syntaxNode is InvocationExpressionSyntax invocationExpressionSyntax)
        {
            if (invocationExpressionSyntax.Expression is MemberAccessExpressionSyntax memberAccessExpressionSyntax)
            {
                var memberName = memberAccessExpressionSyntax.Name.Identifier.Text;
                var argumentList = invocationExpressionSyntax.ArgumentList;

                syntaxInvocationResult.MembersAccess.Insert(0, new MemberAccess(argumentList, memberName));

                ParseInvocationExpression(memberAccessExpressionSyntax.Expression, syntaxInvocationResult);
            }
        }
        else if (syntaxNode is IdentifierNameSyntax nameSyntax)
        {
            syntaxInvocationResult.Root = nameSyntax.Identifier.Text;
        }
        else
        {
            throw new NotSupportedException(syntaxNode.ToString());
        }
    }

    private void Diag(DiagnosticDescriptor desc, Location? location, params object?[]? messageArgs)
    {
        _reportDiagnostic(Diagnostic.Create(desc, location, messageArgs));
    }
}