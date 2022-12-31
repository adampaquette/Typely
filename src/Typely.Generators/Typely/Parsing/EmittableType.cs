using Microsoft.CodeAnalysis;

namespace Typely.Generators.Typely.Parsing;

internal class EmittableType
{
    public SyntaxTree SyntaxTree { get; }
    public Type UnderlyingType { get; }    
    public string TypeName { get; }
    public string Name { get; set; }
    public string Namespace { get; set; }
    public ConstructTypeKind ConstructTypeKind { get; set; } = ConstructTypeKind.Struct;
    public List<EmittableValidation> Validations { get; } = new List<EmittableValidation>();
    public EmittableValidation? CurrentValidation { get; set; } = null;

    public EmittableType(SyntaxTree syntaxTree, Type underlyingType, string typeName, string @namespace)
    {
        SyntaxTree = syntaxTree;
        UnderlyingType = underlyingType;
        TypeName = typeName;
        Name = typeName;
        Namespace = @namespace;
    }
}