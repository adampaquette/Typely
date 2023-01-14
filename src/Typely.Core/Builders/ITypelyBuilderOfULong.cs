namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying long type.
/// </summary>
public interface ITypelyBuilderOfULong : ITypelyBuilder<ITypelyBuilderOfULong, IRuleBuilderOfULong, ulong, IFactoryOfULong>,
    IComparableRuleBuilder<IRuleBuilderOfULong, ulong>
{
}

/// <summary>
/// Rule builder of long.
/// </summary>
public interface IRuleBuilderOfULong :
    IRuleBuilder<ITypelyBuilderOfULong, IRuleBuilderOfULong, ulong, IFactoryOfULong>,
    ITypelyBuilderOfULong
{
}

/// <summary>
/// Factory for creating similar value objects of long.
/// </summary>
public interface IFactoryOfULong : ITypelyBuilder<ITypelyBuilderOfULong>
{
}