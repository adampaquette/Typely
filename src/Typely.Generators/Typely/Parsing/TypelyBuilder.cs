using Microsoft.CodeAnalysis;
using static Typely.Generators.TypelyGenerator;
using Typely.Core;

namespace Typely.Generators.Typely.Parsing;

internal class TypelyBuilder : ITypelyBuilder
{
    private List<EmittableType> _emittableTypes = new List<EmittableType>();
    private SyntaxTree _syntaxTree;
    private readonly Type _configurationType;

    public TypelyBuilder(SyntaxTree syntaxTree, Type configurationType)
    {
        _syntaxTree = syntaxTree;
        _configurationType = configurationType;
    }

    public ITypelyBuilder<T> For<T>(string typeName)
    {
        var emittableType = new EmittableType
        {
            UnderlyingType = typeof(T),
            SyntaxTree = _syntaxTree,
            Name = typeName,
            Namespace = _configurationType.Namespace
        };
        _emittableTypes.Add(emittableType);

        return new RuleBuilder<T>(emittableType);
    }

    public IReadOnlyList<EmittableType> GetEmittableTypes() =>
        _emittableTypes.ToList().AsReadOnly();
}

