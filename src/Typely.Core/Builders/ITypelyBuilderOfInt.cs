namespace Typely.Core.Builders;

public interface ITypelyBuilderOfInt : ITypelyBuilder<int, IRuleBuilderOfInt, ITypelyBuilderOfInt>
{
    IRuleBuilderOfInt Length(int min, int max); //string
    IRuleBuilderOfInt Length(int exactLength); //string
    IRuleBuilderOfInt MinLength(int minLength); //string
    IRuleBuilderOfInt MaxLength(int maxLength); //string
    //IRuleBuilderOfInt Matches(string regex); //string
    IRuleBuilderOfInt LessThan(int value); //IComparable
    IRuleBuilderOfInt LessThanOrEqual(int value); //IComparable
    IRuleBuilderOfInt GreaterThan(int value); //IComparable
    IRuleBuilderOfInt GreaterThanOrEqual(int value); //IComparable    
    IRuleBuilderOfInt InclusiveBetween(int min, int max); //INumber
    IRuleBuilderOfInt ExclusiveBetween(int min, int max); //INumber
    //IRuleBuilderOfInt PrecisionScale(int precision, int scale); //INumber
}

public interface IRuleBuilderOfInt : 
    IRuleBuilder<int, IRuleBuilderOfInt>, 
    ITypelyBuilder<int, IRuleBuilderOfInt, ITypelyBuilderOfInt>
{   
}