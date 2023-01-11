namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying bool type.
/// </summary>
public interface ITypelyBuilderOfBool : ITypelyBuilder<ITypelyBuilderOfBool, IRuleBuilderOfBool, bool, IFactoryOfBool>,
    IComparableRuleBuilder<IRuleBuilderOfBool, bool>
{
}

/// <summary>
/// Rule builder of bool.
/// </summary>
public interface IRuleBuilderOfBool :
    IRuleBuilder<ITypelyBuilderOfBool, IRuleBuilderOfBool, bool, IFactoryOfBool>,
    ITypelyBuilderOfBool
{
}

/// <summary>
/// Factory for creating similar value objects of bool.
/// </summary>
public interface IFactoryOfBool : ITypelyFactory<ITypelyBuilderOfBool>
{
}