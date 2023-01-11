namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying DateTimeOffset type.
/// </summary>
public interface ITypelyBuilderOfDateTimeOffset : ITypelyBuilder<ITypelyBuilderOfDateTimeOffset, IRuleBuilderOfDateTimeOffset, DateTimeOffset, IFactoryOfDateTimeOffset>,
    IComparableRuleBuilder<IRuleBuilderOfDateTimeOffset, DateTimeOffset>
{
}

/// <summary>
/// Rule builder of DateTimeOffset.
/// </summary>
public interface IRuleBuilderOfDateTimeOffset :
    IRuleBuilder<ITypelyBuilderOfDateTimeOffset, IRuleBuilderOfDateTimeOffset, DateTimeOffset, IFactoryOfDateTimeOffset>,
    ITypelyBuilderOfDateTimeOffset
{
}

/// <summary>
/// Factory for creating similar value objects of DateTimeOffset.
/// </summary>
public interface IFactoryOfDateTimeOffset : ITypelyFactory<ITypelyBuilderOfDateTimeOffset>
{
}