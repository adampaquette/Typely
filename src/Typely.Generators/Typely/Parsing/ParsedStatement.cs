using Microsoft.CodeAnalysis;

namespace Typely.Generators.Typely.Parsing;

internal class ParsedStatement
{
    public ParsedStatement(SemanticModel semanticModel)
    {
        SemanticModel = semanticModel;
    }

    public string Root { get; set; } = string.Empty;
    public SemanticModel SemanticModel { get; set; }

    public List<ParsedInvocation> Invocations { get; set; } = new();
}