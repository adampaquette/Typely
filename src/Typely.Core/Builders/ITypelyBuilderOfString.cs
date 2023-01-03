namespace Typely.Core.Builders;

public interface ITypelyBuilderOfString : ITypelyBuilder<string, IRuleBuilderOfString, ITypelyBuilderOfString>
{
    IRuleBuilderOfString Length(int min, int max); //string
    IRuleBuilderOfString Length(int exactLength); //string
    IRuleBuilderOfString MinLength(int minLength); //string
    IRuleBuilderOfString MaxLength(int maxLength); //string
    //IRuleBuilderOfString Matches(string regex); //string
    IRuleBuilderOfString LessThan(string value); //IComparable
    IRuleBuilderOfString LessThanOrEqual(string value); //IComparable
    IRuleBuilderOfString GreaterThan(string value); //IComparable
    IRuleBuilderOfString GreaterThanOrEqual(string value); //IComparable
}

public interface IRuleBuilderOfString :
    IRuleBuilder<string, IRuleBuilderOfString>,
    ITypelyBuilder<string, IRuleBuilderOfString, ITypelyBuilderOfString>
{
}