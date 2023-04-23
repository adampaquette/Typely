using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Typely.Generators.Typely.Emitting;

namespace Typely.Generators.Typely.Parsing;

internal static class ParsedInvocationExtensions
{
    public static string GetFirstStringArgument(this ParsedInvocation parsedInvocation)
    {
        var value = parsedInvocation.ArgumentListSyntax.Arguments.First().ToString();
        return value.Substring(1, value.Length - 2);
    }

    public static string GetFirstArgument(this ParsedInvocation parsedInvocation) =>
        parsedInvocation.ArgumentListSyntax.Arguments.First().Expression.ToString();

    public static string GetSecondArgument(this ParsedInvocation parsedInvocation) =>
        parsedInvocation.ArgumentListSyntax.Arguments[1].Expression.ToString();

    public static ExpressionSyntax GetFirstExpressionArgument(this ParsedInvocation parsedInvocation) =>
        parsedInvocation.ArgumentListSyntax.Arguments.First().Expression;

    public static string GetLambdaBodyOfFirstArgument(this ParsedInvocation parsedInvocation)
    {
        var expression = parsedInvocation.ArgumentListSyntax.Arguments.First().Expression;
        return expression switch
        {
            SimpleLambdaExpressionSyntax simpleLambda =>
                GetLambdaBodyWithReplacedParameter(simpleLambda, Emitter.ValueParameterName).ToString(),
            ParenthesizedLambdaExpressionSyntax parenthesizedlambda =>
                GetLambdaBodyWithReplacedParameter(parenthesizedlambda, Emitter.ValueParameterName).ToString(),
            _ => throw new InvalidOperationException("Expected lambda expression but was " + expression.GetType().Name)
        };
    }

    private static CSharpSyntaxNode GetLambdaBodyWithReplacedParameter(SimpleLambdaExpressionSyntax lambda,
        string newParameterName)
    {
        var originalParameter = lambda.Parameter;

        return lambda.Body.ReplaceNodes(lambda.Body.DescendantNodes().OfType<IdentifierNameSyntax>(),
            (originalNode, rewrittenNode) =>
            {
                if (originalNode.Identifier.Text == originalParameter.Identifier.Text)
                {
                    var newIdentifier = SyntaxFactory.Identifier(originalNode.Identifier.LeadingTrivia,
                        newParameterName, originalNode.Identifier.TrailingTrivia);

                    return rewrittenNode.WithIdentifier(newIdentifier);
                }

                return rewrittenNode;
            });
    }

    private static CSharpSyntaxNode GetLambdaBodyWithReplacedParameter(ParenthesizedLambdaExpressionSyntax lambda,
        string newParameterName)
    {
        var originalParameter = lambda.ParameterList.Parameters.First();

        return lambda.Body.ReplaceNodes(lambda.Body.DescendantNodes().OfType<IdentifierNameSyntax>(),
            (originalNode, rewrittenNode) =>
            {
                if (originalNode.Identifier.Text == originalParameter.Identifier.Text)
                {
                    var newIdentifier = SyntaxFactory.Identifier(originalNode.Identifier.LeadingTrivia,
                        newParameterName, originalNode.Identifier.TrailingTrivia);

                    return rewrittenNode.WithIdentifier(newIdentifier);
                }

                return rewrittenNode;
            });
    }
}