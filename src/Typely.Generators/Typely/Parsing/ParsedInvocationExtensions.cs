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

    public static string GetFirstNumberArgument(this ParsedInvocation parsedInvocation) =>
        parsedInvocation.ArgumentListSyntax.Arguments.First().Expression.ToString();

    public static ExpressionSyntax GetFirstExpressionArgument(this ParsedInvocation parsedInvocation) =>
        parsedInvocation.ArgumentListSyntax.Arguments.First().Expression;

    public static string GetLambdaBodyOfFirstArgument(this ParsedInvocation parsedInvocation)
    {
        var expression = parsedInvocation.ArgumentListSyntax.Arguments.First().Expression;
        return expression switch
        {
            SimpleLambdaExpressionSyntax simpleLambda => ReplaceParameter(simpleLambda, Emitter.ValueParameterName).Body.ToString(),
            ParenthesizedLambdaExpressionSyntax parenthesizedlambda => ReplaceParameter(parenthesizedlambda, Emitter.ValueParameterName).Body.ToString(),
            _ => throw new InvalidOperationException("Expected lambda expression but was " + expression.GetType().Name)
        };
    }


    static LambdaExpressionSyntax ReplaceParameter(LambdaExpressionSyntax lambda, string newParameterName)
    {
        switch (lambda)
        {
            case SimpleLambdaExpressionSyntax simpleLambda:
                return ReplaceParameterInSimpleLambda(simpleLambda, newParameterName);

            case ParenthesizedLambdaExpressionSyntax parenthesizedLambda:
                return ReplaceParameterInParenthesizedLambda(parenthesizedLambda, newParameterName);

            default:
                throw new NotSupportedException("Unsupported lambda expression type.");
        }
    }

    static SimpleLambdaExpressionSyntax ReplaceParameterInSimpleLambda(SimpleLambdaExpressionSyntax lambda, string newParameterName)
    {
        var originalParameter = lambda.Parameter;
        var newParameter = originalParameter.WithIdentifier(SyntaxFactory.Identifier(newParameterName));

        var newBody = lambda.Body.ReplaceNodes(lambda.Body.DescendantNodes().OfType<IdentifierNameSyntax>(), (originalNode, rewrittenNode) =>
        {
            if (originalNode.Identifier.Text == originalParameter.Identifier.Text)
            {
                return rewrittenNode.WithIdentifier(SyntaxFactory.Identifier(newParameterName));
            }
            return rewrittenNode;
        });

        return lambda.WithParameter(newParameter).WithBody(newBody);
    }

    static ParenthesizedLambdaExpressionSyntax ReplaceParameterInParenthesizedLambda(ParenthesizedLambdaExpressionSyntax lambda, string newParameterName)
    {
        var originalParameter = lambda.ParameterList.Parameters.First();
        var newParameter = originalParameter.WithIdentifier(SyntaxFactory.Identifier(newParameterName));

        var newParameterList = lambda.ParameterList.ReplaceNode(originalParameter, newParameter);

        var newBody = lambda.Body.ReplaceNodes(lambda.Body.DescendantNodes().OfType<IdentifierNameSyntax>(), (originalNode, rewrittenNode) =>
        {
            if (originalNode.Identifier.Text == originalParameter.Identifier.Text)
            {
                return rewrittenNode.WithIdentifier(SyntaxFactory.Identifier(newParameterName));
            }
            return rewrittenNode;
        });

        return lambda.WithParameterList(newParameterList).WithBody(newBody);
    }
}