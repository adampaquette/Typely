namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying float type.
/// </summary>
public interface ITypelyBuilderOfFloat : ITypelyBuilder<ITypelyBuilderOfFloat, IRuleBuilderOfFloat, float, IFactoryOfFloat>,
    IComparableRuleBuilder<IRuleBuilderOfFloat, float>
{
}

/// <summary>
/// Rule builder of float.
/// </summary>
public interface IRuleBuilderOfFloat :
    IRuleBuilder<ITypelyBuilderOfFloat, IRuleBuilderOfFloat, float, IFactoryOfFloat>,
    ITypelyBuilderOfFloat
{
}

/// <summary>
/// Factory for creating similar value objects of float.
/// </summary>
public interface IFactoryOfFloat : ITypelyFactory<ITypelyBuilderOfFloat>
{
}