using System.Linq.Expressions;

namespace Typely.Core.Builders;

public interface ITypelyBuilder
{
    ITypelyBuilder<T> Of<T>();
    ITypelyBuilderOfInt Int();
    ITypelyBuilderOfString String();
}

public interface ITypelyBuilder<T> : ITypelyBuilder<T, IRuleBuilder, ITypelyBuilder<T>>
{
}

public interface IRuleBuilder :
    IRuleBuilder<int, IRuleBuilderOfInt>,
    ITypelyBuilder<int, IRuleBuilderOfInt, ITypelyBuilderOfInt>
{
}

public interface ITypelyBuilder<TValue, TRuleBuilder, TThis> 
    where TThis : ITypelyBuilder<TValue, TRuleBuilder, TThis>
{
    TThis For(string typeName);
    //TThis Namespace(string value);
    //TThis AsStruct();
    ////TThis AsClass();
    //TThis Name(string name);
    //TThis Name(Expression<Func<string>> expression);

    TRuleBuilder NotEmpty(); //T
    //TRuleBuilder NotEqual(TValue value); //T
    //IRuleBuilder<T, TThis> Must(Expression<Func<T, bool>> predicate); //T
}

public interface IRuleBuilder<T, TThis> : ITypelyBuilder<T, TThis, TThis> 
    where TThis : IRuleBuilder<T, TThis>
{
    IRuleBuilder<T, TThis> WithErrorCode(string errorCode);
    //IRuleBuilder<T, TThis> WithMessage(string message);
    //IRuleBuilder<T, TThis> WithMessage(Expression<Func<string>> expression);
}


//public interface ITypelyBuilder<T>
//{
//    ITypelyBuilder<T> For(string typeName);
//    ITypelyBuilder<T> Namespace(string value);
//    ITypelyBuilder<T> AsStruct();
//    //ITypelyBuilder<T> AsClass();
//    ITypelyBuilder<T> Name(string name);
//    ITypelyBuilder<T> Name(Expression<Func<string>> expression);

//    IRuleBuilder<T> NotEmpty(); //T
//    IRuleBuilder<T> NotEqual(T value); //T
//    //IRuleBuilder<T> Must(Expression<Func<T, bool>> predicate); //T
//    IRuleBuilder<T> Length(int min, int max); //string
//    IRuleBuilder<T> Length(int exactLength); //string
//    IRuleBuilder<T> MinLength(int minLength); //string
//    IRuleBuilder<T> MaxLength(int maxLength); //string
//    //IRuleBuilder<T> Matches(string regex); //string
//    IRuleBuilder<T> LessThan(T value); //IComparable
//    IRuleBuilder<T> LessThanOrEqual(T value); //IComparable
//    IRuleBuilder<T> GreaterThan(T value); //IComparable
//    IRuleBuilder<T> GreaterThanOrEqual(T value); //IComparable    
//    IRuleBuilder<T> InclusiveBetween(int min, int max); //INumber
//    IRuleBuilder<T> ExclusiveBetween(int min, int max); //INumber
//    //IRuleBuilder<T> PrecisionScale(int precision, int scale); //INumber
//}

//public interface IRuleBuilder<T> : ITypelyBuilder<T>
//{
//    IRuleBuilder<T> WithErrorCode(string errorCode);
//    IRuleBuilder<T> WithMessage(string message);
//    IRuleBuilder<T> WithMessage(Expression<Func<string>> expression);

//    //IRuleBuilder<T> WithErrorCode(string errorCode);
//}