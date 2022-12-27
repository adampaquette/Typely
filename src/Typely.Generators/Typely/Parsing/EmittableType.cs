using Microsoft.CodeAnalysis;

namespace Typely.Generators.Typely.Parsing;

internal class EmittableType
{
    public SyntaxTree? SyntaxTree { get; set; }
    public Type? UnderlyingType { get; set; }
    public string? Name { get; set; }
    public string? Namespace { get; set; }
}

