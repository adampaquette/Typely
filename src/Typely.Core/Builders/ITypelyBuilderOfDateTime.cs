namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying DateTime type.
/// </summary>
public interface ITypelyBuilderOfDateTime : ITypelyBuilder<ITypelyBuilderOfDateTime, IRuleBuilderOfDateTime, DateTime, IFactoryOfDateTime>,
    IComparableRuleBuilder<IRuleBuilderOfDateTime, DateTime>
{
}

/// <summary>
/// Rule builder of DateTime.
/// </summary>
public interface IRuleBuilderOfDateTime :
    IRuleBuilder<ITypelyBuilderOfDateTime, IRuleBuilderOfDateTime, DateTime, IFactoryOfDateTime>,
    ITypelyBuilderOfDateTime
{
}

/// <summary>
/// Factory for creating similar value objects of DateTime.
/// </summary>
public interface IFactoryOfDateTime : ITypelyBuilder<ITypelyBuilderOfDateTime>
{
}