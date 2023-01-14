namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying long type.
/// </summary>
public interface ITypelyBuilderOfLong : ITypelyBuilder<ITypelyBuilderOfLong, IRuleBuilderOfLong, long, IFactoryOfLong>,
    IComparableRuleBuilder<IRuleBuilderOfLong, long>
{
}

/// <summary>
/// Rule builder of long.
/// </summary>
public interface IRuleBuilderOfLong :
    IRuleBuilder<ITypelyBuilderOfLong, IRuleBuilderOfLong, long, IFactoryOfLong>,
    ITypelyBuilderOfLong
{
}

/// <summary>
/// Factory for creating similar value objects of long.
/// </summary>
public interface IFactoryOfLong : ITypelyBuilder<ITypelyBuilderOfLong>
{
}