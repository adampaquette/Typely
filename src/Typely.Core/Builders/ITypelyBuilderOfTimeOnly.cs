namespace Typely.Core.Builders;

#if NET7_0_OR_GREATER
/// <summary>
/// Builder dedicated to creating a value object with the underlying TimeOnly type.
/// </summary>
public interface ITypelyBuilderOfTimeOnly : ITypelyBuilder<ITypelyBuilderOfTimeOnly, IRuleBuilderOfTimeOnly, TimeOnly, IFactoryOfTimeOnly>,
    IComparableRuleBuilder<IRuleBuilderOfTimeOnly, TimeOnly>
{
}

/// <summary>
/// Rule builder of TimeOnly.
/// </summary>
public interface IRuleBuilderOfTimeOnly :
    IRuleBuilder<ITypelyBuilderOfTimeOnly, IRuleBuilderOfTimeOnly, TimeOnly, IFactoryOfTimeOnly>,
    ITypelyBuilderOfTimeOnly
{
}

/// <summary>
/// Factory for creating similar value objects of TimeOnly.
/// </summary>
public interface IFactoryOfTimeOnly : ITypelyFactory<ITypelyBuilderOfTimeOnly>
{
}
#endif