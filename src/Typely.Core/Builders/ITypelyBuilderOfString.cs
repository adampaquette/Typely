using System.Linq.Expressions;

namespace Typely.Core.Builders;

public interface ITypelyBuilderOfString
{
    ITypelyBuilderOfString For(string typeName);
    ITypelyBuilderOfString Namespace(string value);
    ITypelyBuilderOfString AsStruct();
    //ITypelyBuilderOfString AsClass();
    ITypelyBuilderOfString Name(string name);
    ITypelyBuilderOfString Name(Expression<Func<string>> expression);

    IRuleBuilderOfString NotEmpty(); //T
    IRuleBuilderOfString NotEqual(string value); //T
    IRuleBuilderOfString Must(Expression<Func<string, bool>> predicate); //T
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

public interface IRuleBuilderOfString : ITypelyBuilderOfString
{
    IRuleBuilderOfString WithErrorCode(string errorCode);
    IRuleBuilderOfString WithMessage(string message);
    IRuleBuilderOfString WithMessage(Expression<Func<string>> expression);
}