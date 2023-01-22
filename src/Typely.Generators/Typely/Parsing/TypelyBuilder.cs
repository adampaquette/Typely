using Typely.Core.Builders;
using Typely.Generators.Typely.Parsing.Int;
using Typely.Generators.Typely.Parsing.String;

namespace Typely.Generators.Typely.Parsing;

/// <summary>
/// Implementation of the <see cref="ITypelyBuilder"/> to create metadata about types to be generated.
/// </summary>
internal class TypelyBuilder : ITypelyBuilder
{
    private readonly List<EmittableType> _emittableTypes = new();
    private readonly string _defaultNamespace;

    public TypelyBuilder(string defaultNamespace)
    {
        _defaultNamespace = defaultNamespace;
    }

    public IReadOnlyList<EmittableType> GetEmittableTypes() => _emittableTypes.AsReadOnly();

    public ITypelyBuilderOfInt OfInt()
    {
        var emittableType = new EmittableType(
            underlyingType: typeof(int),
            defaultNamespace: _defaultNamespace);

        _emittableTypes.Add(emittableType);

        return new RuleBuilderOfInt(emittableType, _emittableTypes);
    }

    public ITypelyBuilderOfString OfString()
    {
        var emittableType = new EmittableType(
            underlyingType: typeof(string),
            defaultNamespace: _defaultNamespace);

        _emittableTypes.Add(emittableType);

        return new RuleBuilderOfString(emittableType, _emittableTypes);
    }
}