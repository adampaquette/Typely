﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using Typely.Generators.Extensions;
using Typely.Generators.Typely.Parsing.TypeBuilders;

namespace Typely.Generators.Typely.Parsing;

/// <summary>
/// Parse a <see cref="ClassDeclarationSyntax"/> and generates a list of <see cref="EmittableTypeBuilder"/>.
/// </summary>
internal static class Parser
{
    /// <summary>
    /// Filter classes having an interface name "ITypelyConfiguration".
    /// </summary>
    internal static bool IsTypelyConfigurationClass(SyntaxNode syntaxNode, CancellationToken cancellationToken) =>
        syntaxNode is ClassDeclarationSyntax c && IsTypelyConfigurationClass(c);

    /// <summary>
    /// Filter classes having an interface name "ITypelyConfiguration".
    /// </summary>
    private static bool IsTypelyConfigurationClass(ClassDeclarationSyntax syntax) =>
        syntax.HasInterface(TypelyConfiguration.InterfaceName);

    private static bool IsConfigureMethod(SyntaxNode syntaxNode) =>
        syntaxNode is MethodDeclarationSyntax c && c.Identifier.Text == TypelyConfiguration.ConfigureMethodName;

    /// <summary>
    /// Filter classes having an interface "ITypelyConfiguration" that matches the 
    /// namespace and returns the <see cref="GeneratorClassContext"/>.
    /// </summary>
    internal static GeneratorClassContext? GetSemanticTargetForGeneration(GeneratorSyntaxContext context,
        CancellationToken cancellationToken)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;
        var classSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax)!;

        return classSymbol.AllInterfaces.Any(x => x.ToString() == TypelyConfiguration.FullInterfaceName)
            ? new GeneratorClassContext(classDeclarationSyntax, context.SemanticModel)
            : null;
    }

    /// <summary>
    /// Execute the different "ITypelyConfiguration" classes and generate models of the desired user types.
    /// </summary>
    /// <param name="context">The generator's context.</param>
    /// <param name="cancellationToken">A token to notify the operation should be cancelled.</param>
    /// <returns>A list of representation of desired user types.</returns>
    internal static ImmutableArray<EmittableType> GetEmittableTypes(GeneratorClassContext? context,
        CancellationToken cancellationToken) 
    {
        var emittableTypes = new List<EmittableType>();
        var classSyntaxes = context!.Value.ClassDeclarationSyntax
            .SyntaxTree
            .GetRoot()
            .DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .Where(IsTypelyConfigurationClass)
            .ToList();

        foreach (var classSyntax in classSyntaxes)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var classEmittableTypes = ParseClass(classSyntax, context.Value.SemanticModel);
            emittableTypes.AddRange(classEmittableTypes);
        }

        return emittableTypes.ToImmutableArray();
    }

    /// <summary>
    /// Parse a <see cref="ClassDeclarationSyntax"/> and generate a list of <see cref="EmittableType"/>.
    /// </summary>
    /// <param name="classSyntax">The class to parse.</param>
    /// <param name="model">The <see cref="SemanticModel"/>.</param>
    /// <returns>A list of <see cref="EmittableType"/>.</returns>
    private static IEnumerable<EmittableType> ParseClass(ClassDeclarationSyntax classSyntax, SemanticModel model)
    {
        var methodSyntax = classSyntax.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .First(IsConfigureMethod);

        var emittableTypes = new List<EmittableType>();
        var typelyBuilderParameterName = methodSyntax.ParameterList.Parameters.First().Identifier.Text;
        var parsedStatements = ParseStatements(methodSyntax, typelyBuilderParameterName, model);
        var defaultNamespace = GetNamespace(classSyntax);

        foreach (var parsedStatement in parsedStatements)
        {
            var emittableType = EmittableTypeBuilderFactory.Create(defaultNamespace, parsedStatement).Build();
            emittableTypes.Add(emittableType);
        }

        return emittableTypes;
    }

    /// <summary>
    /// Parse each line of code of a method.
    /// </summary>
    /// <param name="methodDeclarationSyntax">The <see cref="MethodDeclarationSyntax"/>.</param>
    /// <param name="typelyBuilderParameterName">The builder parameter name used in "ITypelyConfiguration.Configure".</param>
    /// <param name="model">The <see cref="SemanticModel"/>.</param>
    /// <returns>Return a list of <see cref="ParseDeclarationStatement"/>.</returns>
    private static List<ParsedStatement> ParseStatements(MethodDeclarationSyntax methodDeclarationSyntax,
        string typelyBuilderParameterName, SemanticModel model)
    {
        var bodySyntaxNodes = methodDeclarationSyntax.Body!.DescendantNodes()
            .Where(x => x is ExpressionStatementSyntax || x is LocalDeclarationStatementSyntax);
        var parsedStatements = new List<ParsedStatement>();
        var parsedStatementVariables = new Dictionary<string, ParsedStatement>();

        foreach (var bodySyntaxNode in bodySyntaxNodes)
        {
            var parsedExpression = new ParsedStatement(model);

            if (bodySyntaxNode is ExpressionStatementSyntax expressionStatementSyntax)
            {
                parsedStatements.Add(parsedExpression);
                ParseInvocationExpression(expressionStatementSyntax.Expression, parsedExpression);
            }
            else if (bodySyntaxNode is LocalDeclarationStatementSyntax localDeclarationStatementSyntax)
            {
                ParseDeclarationStatement(parsedStatementVariables, parsedExpression, localDeclarationStatementSyntax);
            }

            if (DoesNotUseBuilderParameter(parsedExpression))
            {
                MergeVariableInvocations(parsedStatementVariables, parsedExpression);
            }
        }

        return parsedStatements;

        bool DoesNotUseBuilderParameter(ParsedStatement invocationResult) =>
            invocationResult.Root != typelyBuilderParameterName;
    }

    /// <summary>
    /// Parse a <see cref="LocalDeclarationStatementSyntax"/>.
    /// ex: var vote = builder.OfInt().For("Vote");
    /// </summary>
    private static void ParseDeclarationStatement(
        Dictionary<string, ParsedStatement> parsedExpressionVariables,
        ParsedStatement parsed, LocalDeclarationStatementSyntax localDeclarationStatementSyntax)
    {
        var variable = localDeclarationStatementSyntax.Declaration.Variables.First();
        parsedExpressionVariables.Add(variable.Identifier.Text, parsed);
        ParseInvocationExpression(variable.Initializer!.Value, parsed);
    }

    /// <summary>
    /// Combine the invocations of the root variable with the current parsed expression.
    /// </summary>
    /// <param name="parsedExpressionVariables">List of variables containing a series of invocations.</param>
    /// <param name="parsedStatement">A line of code already parsed.</param>
    private static void MergeVariableInvocations(
        Dictionary<string, ParsedStatement> parsedExpressionVariables,
        ParsedStatement parsedStatement)
    {
        var parsedExpressionVariable = parsedExpressionVariables[parsedStatement.Root];

        parsedStatement.Invocations.InsertRange(0, parsedExpressionVariable.Invocations);
        parsedStatement.Root = parsedExpressionVariable.Root;
    }

    /// <summary>
    /// Get the namespace of a <see cref="SyntaxNode"/>.
    /// </summary>
    private static string GetNamespace(SyntaxNode classSyntax)
    {
        while (classSyntax.Parent != null)
        {
            if (classSyntax.Parent is NamespaceDeclarationSyntax namespaceDeclarationSyntax)
            {
                return namespaceDeclarationSyntax.Name.ToString();
            }

            if (classSyntax.Parent is FileScopedNamespaceDeclarationSyntax fileScopedNamespaceDeclarationSyntax)
            {
                return fileScopedNamespaceDeclarationSyntax.Name.ToString();
            }

            classSyntax = classSyntax.Parent;
        }

        return string.Empty;
    }

    /// <summary>
    /// Parse a <see cref="SyntaxNode"/> as an <see cref="InvocationExpressionSyntax"/> to get the member name and the argument list.
    /// </summary>
    private static void ParseInvocationExpression(ExpressionSyntax syntaxNode,
        ParsedStatement parsed)
    {
        // ex: builder.OfInt().For("Vote").WithNamespace("UserAggregate").WithName("Vote")
        if (syntaxNode is InvocationExpressionSyntax invocationExpressionSyntax)
        {
            if (invocationExpressionSyntax.Expression is MemberAccessExpressionSyntax memberAccessExpressionSyntax)
            {
                var memberName = memberAccessExpressionSyntax.Name.Identifier.Text;
                var argumentList = invocationExpressionSyntax.ArgumentList;

                parsed.Invocations.Insert(0, new ParsedInvocation(argumentList, memberName));

                // ex: builder.OfInt().For("Vote").WithNamespace("UserAggregate").WithName()
                ParseInvocationExpression(memberAccessExpressionSyntax.Expression, parsed);
            }
        }
        // ex: builder
        else if (syntaxNode is IdentifierNameSyntax nameSyntax)
        {
            parsed.Root = nameSyntax.Identifier.Text;
        }
        else
        {
            throw new NotSupportedException(syntaxNode.ToString());
        }
    }
}