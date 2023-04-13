using Microsoft.CodeAnalysis.CSharp.Syntax;

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
            SimpleLambdaExpressionSyntax simpleLambda => simpleLambda.Body.ToString(),
            ParenthesizedLambdaExpressionSyntax parenthesizedlambda => parenthesizedlambda.Body.ToString(),
            _ => throw new InvalidOperationException("Expected lambda expression but was " + expression.GetType().Name)
        };
    }
}