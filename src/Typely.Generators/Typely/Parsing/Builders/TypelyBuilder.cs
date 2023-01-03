using Microsoft.CodeAnalysis;
using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing.Builders;

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

    public IReadOnlyList<EmittableType> GetEmittableTypes() => _emittableTypes.AsReadOnly();

    public ITypelyBuilderOfInt Int()
    {
        throw new NotImplementedException();
    }

    public ITypelyBuilderOfString String()
    {
        throw new NotImplementedException();
    }

    public ITypelyBuilder<T> Of<T>()
    {
        throw new NotImplementedException();
        //    var emittableType = new EmittableType(
        //        syntaxTree: _syntaxTree,
        //        underlyingType: typeof(TValue),
        //        @namespace: _configurationType.Namespace);

        //    _emittableTypes.Add(emittableType);

        //    return new RuleBuilder<T>(emittableType, _emittableTypes);
    }
}

