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

    private const string AsFactory = "AsFactory";

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
            ParseClass(emittableTypes, classSyntax);
        }

        return emittableTypes;
    }

    private void ParseClass(List<EmittableType> emittableTypes, SyntaxNode? classSyntax)
    {
        var methodSyntax = classSyntax.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .FirstOrDefault(IsConfigureMethod);

        var typelyBuilderParameterName = methodSyntax.ParameterList.Parameters.First().Identifier.Text;
        var parsedExpressionStatements = ParseStatements(methodSyntax, typelyBuilderParameterName);
        var defaultNamespace = GetNamespace(classSyntax);

        foreach (var parsedExpressionStatement in parsedExpressionStatements)
        {
            var invocationEmittableTypes = EmittableTypeBuilderFactory.Create(defaultNamespace, parsedExpressionStatement).Parse();
            emittableTypes.AddRange(invocationEmittableTypes);
        }
    }

    private static List<ParsedExpressionStatement> ParseStatements(MethodDeclarationSyntax methodDeclarationSyntax, string typelyBuilderParameterName)
    {
        if (methodDeclarationSyntax == null || methodDeclarationSyntax.Body == null)
        {
            return Enumerable.Empty<ParsedExpressionStatement>().ToList();
        }

        var bodySyntaxNodes = methodDeclarationSyntax.Body.DescendantNodes().Where(x => x is ExpressionStatementSyntax || x is LocalDeclarationStatementSyntax);
        var parsedExpressions = new List<ParsedExpressionStatement>();
        var parsedExpressionVariables = new Dictionary<string, ParsedExpressionStatement>();

        foreach (var bodySyntaxNode in bodySyntaxNodes)
        {
            var parsedExpression = new ParsedExpressionStatement();

            if (bodySyntaxNode is ExpressionStatementSyntax expressionStatementSyntax)
            {
                parsedExpressions.Add(parsedExpression);
                ParseInvocationExpression(expressionStatementSyntax.Expression, parsedExpression);
            }
            else if (bodySyntaxNode is LocalDeclarationStatementSyntax localDeclarationStatementSyntax)
            {
                ParseDeclarationStatement(parsedExpressionVariables, parsedExpression, localDeclarationStatementSyntax);
            }

            if (DoesNotUseBuilderParameter(parsedExpression))
            {
                MergeVariableInvocations(parsedExpressionVariables, parsedExpression);
            }
        }

        return parsedExpressions;

        bool DoesNotUseBuilderParameter(ParsedExpressionStatement invocationResult) => invocationResult.Root != typelyBuilderParameterName;
    }
    
    static void ParseDeclarationStatement(Dictionary<string, ParsedExpressionStatement> parsedExpressionVariables, ParsedExpressionStatement parsedExpression, LocalDeclarationStatementSyntax localDeclarationStatementSyntax)
        {
            var variable = localDeclarationStatementSyntax.Declaration.Variables.FirstOrDefault();
            if (variable == null)
            {
                throw new NotSupportedException("Local declaration without variable");
            }

            parsedExpressionVariables.Add(variable.Identifier.Text, parsedExpression);
            if (variable.Initializer == null)
            {
                throw new NotSupportedException("Initializer null for LocalDeclarationStatementSyntax");
            }

            ParseInvocationExpression(variable.Initializer.Value, parsedExpression);
        }

    private static void MergeVariableInvocations(Dictionary<string, ParsedExpressionStatement> parsedExpressionVariables, ParsedExpressionStatement parsedExpression)
    {
        var parsedExpressionVariable = parsedExpressionVariables[parsedExpression.Root];

        parsedExpression.Invocations.InsertRange(0, parsedExpressionVariable.Invocations);
        parsedExpression.Root = parsedExpressionVariable.Root;
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

    private static void ParseInvocationExpression(CSharpSyntaxNode syntaxNode, ParsedExpressionStatement parsedExpression)
    {
        if (syntaxNode is InvocationExpressionSyntax invocationExpressionSyntax)
        {
            if (invocationExpressionSyntax.Expression is MemberAccessExpressionSyntax memberAccessExpressionSyntax)
            {
                var memberName = memberAccessExpressionSyntax.Name.Identifier.Text;
                var argumentList = invocationExpressionSyntax.ArgumentList;

                parsedExpression.Invocations.Insert(0, new ParsedInvocation(argumentList, memberName));

                ParseInvocationExpression(memberAccessExpressionSyntax.Expression, parsedExpression);
            }
        }
        else if (syntaxNode is IdentifierNameSyntax nameSyntax)
        {
            parsedExpression.Root = nameSyntax.Identifier.Text;
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