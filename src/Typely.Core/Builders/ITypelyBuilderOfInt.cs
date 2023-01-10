namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying int type.
/// </summary>
public interface ITypelyBuilderOfInt : ITypelyBuilder<ITypelyBuilderOfInt, IRuleBuilderOfInt, int, IFactoryOfInt>,
    IComparableRuleBuilder<IRuleBuilderOfInt, int>
{
}

/// <summary>
/// Rule builder of int.
/// </summary>
public interface IRuleBuilderOfInt :
    IRuleBuilder<ITypelyBuilderOfInt, IRuleBuilderOfInt, int, IFactoryOfInt>,
    ITypelyBuilderOfInt
{
}

/// <summary>
/// Factory for creating similar value objects of int.
/// </summary>
public interface IFactoryOfInt : ITypelyFactory<ITypelyBuilderOfInt>
{
}