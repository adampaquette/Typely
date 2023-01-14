namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying byte type.
/// </summary>
public interface ITypelyBuilderOfByte : ITypelyBuilder<ITypelyBuilderOfByte, IRuleBuilderOfByte, byte, IFactoryOfByte>,
    IComparableRuleBuilder<IRuleBuilderOfByte, byte>
{
}

/// <summary>
/// Rule builder of byte.
/// </summary>
public interface IRuleBuilderOfByte :
    IRuleBuilder<ITypelyBuilderOfByte, IRuleBuilderOfByte, byte, IFactoryOfByte>,
    ITypelyBuilderOfByte
{
}

/// <summary>
/// Factory for creating similar value objects of byte.
/// </summary>
public interface IFactoryOfByte : ITypelyBuilder<ITypelyBuilderOfByte>
{
}