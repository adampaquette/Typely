namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying char type.
/// </summary>
public interface ITypelyBuilderOfChar : ITypelyBuilder<ITypelyBuilderOfChar, IRuleBuilderOfChar, char, IFactoryOfChar>,
    IComparableRuleBuilder<IRuleBuilderOfChar, char>
{
}

/// <summary>
/// Rule builder of char.
/// </summary>
public interface IRuleBuilderOfChar :
    IRuleBuilder<ITypelyBuilderOfChar, IRuleBuilderOfChar, char, IFactoryOfChar>,
    ITypelyBuilderOfChar
{
}

/// <summary>
/// Factory for creating similar value objects of char.
/// </summary>
public interface IFactoryOfChar : ITypelyBuilder<ITypelyBuilderOfChar>
{
}