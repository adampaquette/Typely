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

    public static string GetLambdaBodyOfFirstArgument(this ParsedInvocation parsedInvocation)
    {
         if(parsedInvocation.ArgumentListSyntax.Arguments.First().Expression is SimpleLambdaExpressionSyntax lambda)
         {
             return lambda.Body.ToString();
         }
         throw new InvalidOperationException("Expected lambda expression");
    }
}