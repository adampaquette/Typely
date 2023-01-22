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
            var syntaxInvocationResults = new List<SyntaxInvocationResult>();
            var syntaxInvocationResultVariables = new Dictionary<string, SyntaxInvocationResult>();
            var bodySyntaxNodes = methodSyntax.Body.DescendantNodes();
            foreach (var bodySyntaxNode in bodySyntaxNodes)
            {
                if (bodySyntaxNode is ExpressionStatementSyntax expressionStatementSyntax)
                {
                    var syntaxInvocationResult = new SyntaxInvocationResult();
                    syntaxInvocationResults.Add(syntaxInvocationResult);
                    ParseInvocationExpression(expressionStatementSyntax.Expression, syntaxInvocationResult);
                }
                else if (bodySyntaxNode is LocalDeclarationStatementSyntax localDeclarationStatementSyntax)
                {
                    var variable = localDeclarationStatementSyntax.Declaration.Variables.FirstOrDefault();
                    if (variable == null)
                    {
                        throw new NotSupportedException("Local declaration without variable");
                    }

                    var syntaxInvocationResult = new SyntaxInvocationResult();
                    syntaxInvocationResultVariables.Add(variable.Identifier.Text, syntaxInvocationResult);
                    if (variable.Initializer == null)
                    {
                        throw new NotSupportedException("Initializer null for LocalDeclarationStatementSyntax");
                    }

                    ParseInvocationExpression(variable.Initializer.Value, syntaxInvocationResult);
                }
            }

            //Phase 2 : Create EmittableTypes
            var configureParameter = methodSyntax.ParameterList.Parameters.First();
            var typelyBuilderParameterName = configureParameter.Identifier.Text;
            var normalSyntaxInvocationResults = syntaxInvocationResults.Where(x => x.Root == typelyBuilderParameterName);

            foreach (var normalSyntaxInvocationResult in normalSyntaxInvocationResults)
            {
                var defaultNamespace = "";
                Type? type = null;
                string typeName = "";
                foreach (var memberAccess in normalSyntaxInvocationResult.MemberAccess)
                {
                    switch (memberAccess.MemberName)
                    {
                        case nameof(ITypelyBuilder.OfString):
                            type = typeof(string);
                            break;
                        case nameof(ITypelyBuilder.OfInt):
                            type = typeof(int);
                            break;
                        case nameof(ITypelyBuilder<int>.For):
                            typeName = memberAccess.ArgumentListSyntax.Arguments.First().ToString();
                            typeName = typeName.Substring(1, typeName.Length - 2);
                            break;
                        default: throw new NotSupportedException(memberAccess.MemberName);
                    }
                }

                if (type == null)
                {
                    throw new Exception("Missing type");
                }

                var emittableType = new EmittableType(type, defaultNamespace);
                emittableType.SetTypeName(typeName);
                emittableTypes.Add(emittableType);
            }
        }
        
        //foreach (var configurationType in configurationTypes)
        //{
        //    var configuration = (ITypelyConfiguration)configurationAssembly.CreateInstance(configurationType.FullName);
        //    var builder = new TypelyBuilder(syntaxTree, configurationType);
        //    configuration.Configure(builder);
        //    emittableTypes.AddRange(builder.GetEmittableTypes());
        //}

        return emittableTypes;
    }

    public void ParseInvocationExpression(CSharpSyntaxNode syntaxNode, SyntaxInvocationResult syntaxInvocationResult)
    {
        if (syntaxNode is InvocationExpressionSyntax invocationExpressionSyntax)
        {
            if (invocationExpressionSyntax.Expression is MemberAccessExpressionSyntax memberAccessExpressionSyntax)
            {
                var memberName = memberAccessExpressionSyntax.Name.Identifier.Text;
                var arguments = invocationExpressionSyntax.ArgumentList;

                syntaxInvocationResult.MemberAccess.Add((memberName, arguments));

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