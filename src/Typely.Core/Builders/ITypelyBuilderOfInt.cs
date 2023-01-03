using System.Linq.Expressions;

namespace Typely.Core.Builders;

public interface ITypelyBuilderOfInt 
{
    ITypelyBuilderOfString For(string typeName);
    ITypelyBuilderOfString Namespace(string value);
    ITypelyBuilderOfString AsStruct();
    //ITypelyBuilderOfString AsClass();
    ITypelyBuilderOfString Name(string name);
    ITypelyBuilderOfString Name(Expression<Func<string>> expression);

    IRuleBuilderOfInt LessThan(int value); //IComparable
    IRuleBuilderOfInt LessThanOrEqual(int value); //IComparable
    IRuleBuilderOfInt GreaterThan(int value); //IComparable
    IRuleBuilderOfInt GreaterThanOrEqual(int value); //IComparable    
    IRuleBuilderOfInt InclusiveBetween(int min, int max); //INumber
    IRuleBuilderOfInt ExclusiveBetween(int min, int max); //INumber
}

public interface IRuleBuilderOfInt : ITypelyBuilderOfInt
{   
}