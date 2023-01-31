namespace Typely.Generators.Typely.Parsing;

internal class ParsedExpressionStatement
{
    public string Root { get; set; } = string.Empty;

    public List<ParsedInvocation> Invocations { get; set; } = new();
}