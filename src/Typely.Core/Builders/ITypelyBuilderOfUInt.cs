namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying uint type.
/// </summary>
public interface ITypelyBuilderOfUInt : ITypelyBuilder<ITypelyBuilderOfUInt, IRuleBuilderOfUInt, uint, IFactoryOfUInt>,
    IComparableRuleBuilder<IRuleBuilderOfUInt, uint>
{
}

/// <summary>
/// Rule builder of uint.
/// </summary>
public interface IRuleBuilderOfUInt :
    IRuleBuilder<ITypelyBuilderOfUInt, IRuleBuilderOfUInt, uint, IFactoryOfUInt>,
    ITypelyBuilderOfUInt
{
}

/// <summary>
/// Factory for creating similar value objects of uint.
/// </summary>
public interface IFactoryOfUInt : ITypelyFactory<ITypelyBuilderOfUInt>
{
}