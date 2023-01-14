namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying decimal type.
/// </summary>
public interface ITypelyBuilderOfDecimal : ITypelyBuilder<ITypelyBuilderOfDecimal, IRuleBuilderOfDecimal, decimal, IFactoryOfDecimal>,
    IComparableRuleBuilder<IRuleBuilderOfDecimal, decimal>
{
}

/// <summary>
/// Rule builder of decimal.
/// </summary>
public interface IRuleBuilderOfDecimal :
    IRuleBuilder<ITypelyBuilderOfDecimal, IRuleBuilderOfDecimal, decimal, IFactoryOfDecimal>,
    ITypelyBuilderOfDecimal
{
}

/// <summary>
/// Factory for creating similar value objects of decimal.
/// </summary>
public interface IFactoryOfDecimal : ITypelyBuilder<ITypelyBuilderOfDecimal>
{
}