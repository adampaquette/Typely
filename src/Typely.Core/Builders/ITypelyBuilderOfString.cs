using System.Text.RegularExpressions;

namespace Typely.Core.Builders;

/// <summary>
/// Builder dedicated to creating a value object with the underlying string type.
/// </summary>
public interface ITypelyBuilderOfString : ITypelyBuilder<ITypelyBuilderOfString, IRuleBuilderOfString, string, IFactoryOfString>,
    IComparableRuleBuilder<IRuleBuilderOfString, string>
{
    IRuleBuilderOfString Length(int min, int max);
    IRuleBuilderOfString Length(int exactLength);
    IRuleBuilderOfString MinLength(int minLength);
    IRuleBuilderOfString MaxLength(int maxLength);
    IRuleBuilderOfString Matches(Regex regex);
}

/// <summary>
/// Rule builder of string.
/// </summary>
public interface IRuleBuilderOfString :
    IRuleBuilder<ITypelyBuilderOfString, IRuleBuilderOfString, string, IFactoryOfString>,
    ITypelyBuilderOfString
{
}

/// <summary>
/// Factory for creating similar value objects of string.
/// </summary>
public interface IFactoryOfString : ITypelyBuilder<ITypelyBuilderOfString>
{
}