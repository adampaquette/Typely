using Microsoft.CodeAnalysis;
using System.Linq.Expressions;

namespace Typely.Generators.Typely.Parsing;

internal class EmittableType
{
    public SyntaxTree SyntaxTree { get; }
    public Type UnderlyingType { get; }
    public string? TypeName { get; private set; }
    public Expression<Func<string>>? Name { get; private set; }
    public string Namespace { get; private set; }
    public ConstructTypeKind ConstructTypeKind { get; private set; } = ConstructTypeKind.Struct;
    public List<EmittableRule> Rules { get; } = new();
    public EmittableRule? CurrentRule { get; set; } = null;

    public EmittableType(SyntaxTree syntaxTree, Type underlyingType, string @namespace)
    {
        SyntaxTree = syntaxTree;
        UnderlyingType = underlyingType;
        Namespace = @namespace;
    }

    public void SetTypeName(string typeName)
    {
        TypeName = typeName;
        Name ??= Expression.Lambda<Func<string>>(Expression.Constant(typeName));
    }

    public void SetName(string name) => Name = Expression.Lambda<Func<string>>(Expression.Constant(name));

    public void WithName(Expression<Func<string>> expression) => Name = expression;

    public void SetNamespace(string value) => Namespace = value;

    public void AsClass() => ConstructTypeKind = ConstructTypeKind.Class;

    public void AsStruct() => ConstructTypeKind = ConstructTypeKind.Struct;

    public void AddRule(EmittableRule emittableRule)
    {
        CurrentRule = emittableRule;
        Rules.Add(emittableRule);
    }

    public EmittableType Clone()
    {
        var emittableType = new EmittableType(SyntaxTree, UnderlyingType, Namespace)
        {
            TypeName = TypeName,
            ConstructTypeKind = ConstructTypeKind,
            CurrentRule = CurrentRule,
            Name = Name
        };
        foreach (var rule in Rules)
        {
            emittableType.Rules.Add(rule);
        }
        return emittableType;
    }
}