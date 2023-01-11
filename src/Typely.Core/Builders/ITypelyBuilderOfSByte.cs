namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying sbyte type.
/// </summary>
public interface ITypelyBuilderOfSByte : ITypelyBuilder<ITypelyBuilderOfSByte, IRuleBuilderOfSByte, sbyte, IFactoryOfSByte>,
    IComparableRuleBuilder<IRuleBuilderOfSByte, sbyte>
{
}

/// <summary>
/// Rule builder of sbyte.
/// </summary>
public interface IRuleBuilderOfSByte :
    IRuleBuilder<ITypelyBuilderOfSByte, IRuleBuilderOfSByte, sbyte, IFactoryOfSByte>,
    ITypelyBuilderOfSByte
{
}

/// <summary>
/// Factory for creating similar value objects of sbyte.
/// </summary>
public interface IFactoryOfSByte : ITypelyFactory<ITypelyBuilderOfSByte>
{
}