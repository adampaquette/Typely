namespace Typely.Core.Builders;

#if NET7_0_OR_GREATER
/// <summary>
/// Builder dedicated to creating a value object with the underlying DateOnly type.
/// </summary>
public interface ITypelyBuilderOfDateOnly : ITypelyBuilder<ITypelyBuilderOfDateOnly, IRuleBuilderOfDateOnly, DateOnly, IFactoryOfDateOnly>,
    IComparableRuleBuilder<IRuleBuilderOfDateOnly, DateOnly>
{
}

/// <summary>
/// Rule builder of DateOnly.
/// </summary>
public interface IRuleBuilderOfDateOnly :
    IRuleBuilder<ITypelyBuilderOfDateOnly, IRuleBuilderOfDateOnly, DateOnly, IFactoryOfDateOnly>,
    ITypelyBuilderOfDateOnly
{
}

/// <summary>
/// Factory for creating similar value objects of DateOnly.
/// </summary>
public interface IFactoryOfDateOnly : ITypelyFactory<ITypelyBuilderOfDateOnly>
{
}
#endif