using Microsoft.CodeAnalysis;
using Typely.Core.Builders;
using Typely.Generators.Typely.Parsing.String;

namespace Typely.Generators.Typely.Parsing;

/// <summary>
/// Implementation of the <see cref="ITypelyBuilder"/> to create metadata about types to be generated.
/// </summary>
internal class TypelyBuilder : ITypelyBuilder
{
    private readonly List<EmittableType> _emittableTypes = new();
    private readonly SyntaxTree _syntaxTree;
    private readonly Type _configurationType;

    public TypelyBuilder(SyntaxTree syntaxTree, Type configurationType)
    {
        _syntaxTree = syntaxTree;
        _configurationType = configurationType;
    }

    public IReadOnlyList<EmittableType> GetEmittableTypes() => _emittableTypes.AsReadOnly();

    /// <inheritdoc/>
    public ITypelyBuilderOfString OfString()
    {
        var emittableType = new EmittableType(
            syntaxTree: _syntaxTree,
            underlyingType: typeof(string),
            @namespace: _configurationType.Namespace);

        _emittableTypes.Add(emittableType);

        return new RuleBuilderOfString(emittableType, _emittableTypes);
    }

    //public ITypelyBuilder<T> Of<T>()
    //{
    //    throw new NotImplementedException();
    //    //    var emittableType = new EmittableType(
    //    //        syntaxTree: _syntaxTree,
    //    //        underlyingType: typeof(TValue),
    //    //        @namespace: _configurationType.Namespace);

    //    //    _emittableTypes.Add(emittableType);

    //    //    return new RuleBuilder<T>(emittableType, _emittableTypes);
    //}
}