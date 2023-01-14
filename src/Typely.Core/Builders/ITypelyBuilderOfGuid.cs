namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying Guid type.
/// </summary>
public interface ITypelyBuilderOfGuid : ITypelyBuilder<ITypelyBuilderOfGuid, IRuleBuilderOfGuid, Guid, IFactoryOfGuid>,
    IComparableRuleBuilder<IRuleBuilderOfGuid, Guid>
{
}

/// <summary>
/// Rule builder of Guid.
/// </summary>
public interface IRuleBuilderOfGuid :
    IRuleBuilder<ITypelyBuilderOfGuid, IRuleBuilderOfGuid, Guid, IFactoryOfGuid>,
    ITypelyBuilderOfGuid
{
}

/// <summary>
/// Factory for creating similar value objects of Guid.
/// </summary>
public interface IFactoryOfGuid : ITypelyBuilder<ITypelyBuilderOfGuid>
{
}