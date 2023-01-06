namespace Typely.Core.Builders;

public interface ITypelyBuilderOfString : ITypelyBuilderBase<ITypelyBuilderOfString, IRuleBuilderOfString, string>
{
    IRuleBuilderOfString Length(int min, int max);
    IRuleBuilderOfString Length(int exactLength);
    IRuleBuilderOfString MinLength(int minLength);
    IRuleBuilderOfString MaxLength(int maxLength);
    //IRuleBuilderOfString Matches(string regex);
    IRuleBuilderOfString LessThan(string value); //IComparable
    IRuleBuilderOfString LessThanOrEqual(string value); //IComparable
    IRuleBuilderOfString GreaterThan(string value); //IComparable
    IRuleBuilderOfString GreaterThanOrEqual(string value); //IComparable
}

public interface IRuleBuilderOfString : IRuleBuilderBase<ITypelyBuilderOfString, IRuleBuilderOfString, string>
{
}