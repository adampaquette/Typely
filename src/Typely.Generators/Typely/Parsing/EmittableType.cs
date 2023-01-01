using Microsoft.CodeAnalysis;
using System.Linq.Expressions;

namespace Typely.Generators.Typely.Parsing;

internal class EmittableType
{
    public SyntaxTree SyntaxTree { get; }
    public Type UnderlyingType { get; }    
    public string TypeName { get; }
    public Expression<Func<string>> Name { get; set; }
    public string Namespace { get; set; }
    public ConstructTypeKind ConstructTypeKind { get; set; } = ConstructTypeKind.Struct;
    public List<EmittableValidation> Validations { get; } = new List<EmittableValidation>();
    public EmittableValidation? CurrentValidation { get; set; } = null;

    public EmittableType(SyntaxTree syntaxTree, Type underlyingType, string typeName, string @namespace)
    {
        SyntaxTree = syntaxTree;
        UnderlyingType = underlyingType;
        TypeName = typeName;
        Name = Expression.Lambda<Func<string>>(Expression.Constant(typeName));
        Namespace = @namespace;
    }
}