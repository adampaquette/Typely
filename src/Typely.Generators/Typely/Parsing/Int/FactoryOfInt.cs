using System.Linq.Expressions;
using Typely.Core.Builders;
using Typely.Generators.Typely.Parsing.String;

namespace Typely.Generators.Typely.Parsing.Int;

/// <summary>
/// Factory for creating similar value objects of string.
/// </summary>
internal class FactoryOfInt : IFactoryOfInt
{
    private readonly EmittableType _emittableType;
    private readonly List<EmittableType> _emittableTypes;

    public FactoryOfInt(EmittableType emittableType, List<EmittableType> emittableTypes)
    {
        _emittableType = emittableType;
        _emittableTypes = emittableTypes;
    }

    /// <inheritdoc/>
    public ITypelyBuilderOfInt For(string typeName) => CreateRuleBuilder().For(typeName);

    /// <inheritdoc/>
    public ITypelyBuilderOfInt AsClass() => CreateRuleBuilder().AsClass();

    /// <inheritdoc/>
    public ITypelyBuilderOfInt AsStruct() => CreateRuleBuilder().AsStruct();

    /// <inheritdoc/>
    public ITypelyBuilderOfInt WithName(string name) => CreateRuleBuilder().WithName(name);

    /// <inheritdoc/>
    public ITypelyBuilderOfInt WithName(Expression<Func<string>> expression) => CreateRuleBuilder().WithName(expression);

    /// <inheritdoc/>
    public ITypelyBuilderOfInt WithNamespace(string value) => CreateRuleBuilder().WithNamespace(value);

    private ITypelyBuilderOfInt CreateRuleBuilder()
    {
        var emittableType = _emittableType.Clone();
        _emittableTypes.Add(emittableType);
        return new RuleBuilderOfInt(emittableType, _emittableTypes);
    }

    public IReadOnlyList<EmittableType> GetEmittableTypes() => _emittableTypes.AsReadOnly();
}