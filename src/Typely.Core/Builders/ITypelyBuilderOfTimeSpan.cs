namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying TimeSpan type.
/// </summary>
public interface ITypelyBuilderOfTimeSpan : ITypelyBuilder<ITypelyBuilderOfTimeSpan, IRuleBuilderOfTimeSpan, TimeSpan, IFactoryOfTimeSpan>,
    IComparableRuleBuilder<IRuleBuilderOfTimeSpan, TimeSpan>
{
}

/// <summary>
/// Rule builder of TimeSpan.
/// </summary>
public interface IRuleBuilderOfTimeSpan :
    IRuleBuilder<ITypelyBuilderOfTimeSpan, IRuleBuilderOfTimeSpan, TimeSpan, IFactoryOfTimeSpan>,
    ITypelyBuilderOfTimeSpan
{
}

/// <summary>
/// Factory for creating similar value objects of TimeSpan.
/// </summary>
public interface IFactoryOfTimeSpan : ITypelyFactory<ITypelyBuilderOfTimeSpan>
{
}