namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying short type.
/// </summary>
public interface ITypelyBuilderOfUShort : ITypelyBuilder<ITypelyBuilderOfUShort, IRuleBuilderOfUShort, ushort, IFactoryOfUShort>,
    IComparableRuleBuilder<IRuleBuilderOfUShort, ushort>
{
}

/// <summary>
/// Rule builder of short.
/// </summary>
public interface IRuleBuilderOfUShort :
    IRuleBuilder<ITypelyBuilderOfUShort, IRuleBuilderOfUShort, ushort, IFactoryOfUShort>,
    ITypelyBuilderOfUShort
{
}

/// <summary>
/// Factory for creating similar value objects of short.
/// </summary>
public interface IFactoryOfUShort : ITypelyFactory<ITypelyBuilderOfUShort>
{
}