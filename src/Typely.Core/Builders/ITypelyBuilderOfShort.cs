namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying short type.
/// </summary>
public interface ITypelyBuilderOfShort : ITypelyBuilder<ITypelyBuilderOfShort, IRuleBuilderOfShort, short, IFactoryOfShort>,
    IComparableRuleBuilder<IRuleBuilderOfShort, short>
{
}

/// <summary>
/// Rule builder of short.
/// </summary>
public interface IRuleBuilderOfShort :
    IRuleBuilder<ITypelyBuilderOfShort, IRuleBuilderOfShort, short, IFactoryOfShort>,
    ITypelyBuilderOfShort
{
}

/// <summary>
/// Factory for creating similar value objects of short.
/// </summary>
public interface IFactoryOfShort : ITypelyFactory<ITypelyBuilderOfShort>
{
}