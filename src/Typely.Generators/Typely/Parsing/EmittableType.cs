using Microsoft.CodeAnalysis;
using System.Linq.Expressions;

namespace Typely.Generators.Typely.Parsing;

internal class EmittableType
{
    public SyntaxTree SyntaxTree { get; }
    public Type UnderlyingType { get; }    
    public string? TypeName { get; set; }
    public Expression<Func<string>>? Name { get; set; }
    public string Namespace { get; set; }
    public ConstructTypeKind ConstructTypeKind { get; set; } = ConstructTypeKind.Struct;
    public List<EmittableRule> Rules { get; } = new List<EmittableRule>();
    public EmittableRule? CurrentRule { get; set; } = null;

    public EmittableType(SyntaxTree syntaxTree, Type underlyingType, string @namespace)
    {
        SyntaxTree = syntaxTree;
        UnderlyingType = underlyingType;        
        Namespace = @namespace;
    }
}