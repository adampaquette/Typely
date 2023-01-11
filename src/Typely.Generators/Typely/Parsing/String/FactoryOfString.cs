using System.Linq.Expressions;
using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing.String;

/// <summary>
/// Factory for creating similar value objects of string.
/// </summary>
internal class FactoryOfString : IFactoryOfString
{
    private readonly EmittableType _emittableType;
    private readonly List<EmittableType> _emittableTypes;

    public FactoryOfString(EmittableType emittableType, List<EmittableType> emittableTypes)
    {
        _emittableType = emittableType;
        _emittableTypes = emittableTypes;
    }

    public ITypelyBuilderOfString For(string typeName) => CreateRuleBuilder().For(typeName);

    public ITypelyBuilderOfString AsClass() => CreateRuleBuilder().AsClass();

    public ITypelyBuilderOfString AsStruct() => CreateRuleBuilder().AsStruct();

    public ITypelyBuilderOfString WithName(string name) => CreateRuleBuilder().WithName(name);

    public ITypelyBuilderOfString WithName(Expression<Func<string>> expression) => CreateRuleBuilder().WithName(expression);

    public ITypelyBuilderOfString WithNamespace(string value) => CreateRuleBuilder().WithNamespace(value);

    private ITypelyBuilderOfString CreateRuleBuilder()
    {
        var emittableType = _emittableType.Clone();
        _emittableTypes.Add(emittableType);
        return new RuleBuilderOfString(emittableType, _emittableTypes);
    }
}
