namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying double type.
/// </summary>
public interface ITypelyBuilderOfDouble : ITypelyBuilder<ITypelyBuilderOfDouble, IRuleBuilderOfDouble, double, IFactoryOfDouble>,
    IComparableRuleBuilder<IRuleBuilderOfDouble, double>
{
}

/// <summary>
/// Rule builder of double.
/// </summary>
public interface IRuleBuilderOfDouble :
    IRuleBuilder<ITypelyBuilderOfDouble, IRuleBuilderOfDouble, double, IFactoryOfDouble>,
    ITypelyBuilderOfDouble
{
}

/// <summary>
/// Factory for creating similar value objects of double.
/// </summary>
public interface IFactoryOfDouble : ITypelyFactory<ITypelyBuilderOfDouble>
{
}