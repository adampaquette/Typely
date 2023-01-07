using System.Linq.Expressions;

namespace Typely.Core.Builders;

public interface ITypelyBuilder
{
    //ITypelyBuilder<T> Of<T>();
    ITypelyBuilderOfInt Int();
    ITypelyBuilderOfString OfString();
}

public interface ITypelyBuilder<TBuilder, TRuleBuilder, TValue, TFactory>
    where TBuilder : ITypelyBuilder<TBuilder, TRuleBuilder, TValue, TFactory>
    where TRuleBuilder : IRuleBuilder<TBuilder, TRuleBuilder, TValue, TFactory>
{
    TBuilder For(string typeName);
    TBuilder AsStruct();
    TBuilder AsClass();
    TBuilder WithNamespace(string value);
    TBuilder WithName(string name);
    TBuilder WithName(Expression<Func<string>> expression);
    TFactory AsFactory();

    TRuleBuilder NotEmpty();
    TRuleBuilder NotEqual(TValue value);
    TRuleBuilder Must(Expression<Func<TValue, bool>> predicate);
}

public interface IRuleBuilder<TBuilder, TRuleBuilder, TValue, TFactory>
    where TBuilder : ITypelyBuilder<TBuilder, TRuleBuilder, TValue, TFactory>
    where TRuleBuilder : IRuleBuilder<TBuilder, TRuleBuilder, TValue, TFactory>
{
    TRuleBuilder WithMessage(string message);
    TRuleBuilder WithMessage(Expression<Func<string>> expression);
    TRuleBuilder WithErrorCode(string errorCode);
}

public interface ITypelyFactory<TBuilder> 
{
    TBuilder For(string typeName);
    TBuilder AsStruct();
    TBuilder AsClass();
    TBuilder WithNamespace(string value);
    TBuilder WithName(string name);
    TBuilder WithName(Expression<Func<string>> expression);
}

public interface IComparableRuleBuilder<TRuleBuilder, TValue>
{
    TRuleBuilder LessThan(TValue value);
    TRuleBuilder LessThanOrEqual(TValue value);
    TRuleBuilder GreaterThan(TValue value);
    TRuleBuilder GreaterThanOrEqual(TValue value);
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