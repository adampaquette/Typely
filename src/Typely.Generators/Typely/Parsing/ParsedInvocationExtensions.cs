namespace Typely.Generators.Typely.Parsing;

internal static class ParsedInvocationExtensions
{
    public static string GetFirstStringArgument(this ParsedInvocation parsedInvocation)
    {
        var value = parsedInvocation.ArgumentListSyntax.Arguments.First().ToString();
        return value.Substring(1, value.Length - 2);
    }
}