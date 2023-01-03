using System.Linq.Expressions;

namespace Typely.Core;

public interface ITypelyBuilder
{
    //public string String { get; }

    ITypelyBuilder<T> Of<T>();
}

public interface ITypelyBuilder<T>
{
    ITypelyBuilder<T> For(string typeName);
    ITypelyBuilder<T> Namespace(string value);
    ITypelyBuilder<T> AsStruct();
    //ITypelyBuilder<T> AsClass();
    ITypelyBuilder<T> Name(string name);
    ITypelyBuilder<T> Name(Expression<Func<string>> expression);

    IRuleBuilder<T> NotEmpty(); //T
    IRuleBuilder<T> NotEqual(T value); //T
    //IRuleBuilder<T> Must(Expression<Func<T, bool>> predicate); //T
    IRuleBuilder<T> Length(int min, int max); //string
    IRuleBuilder<T> Length(int exactLength); //string
    IRuleBuilder<T> MinLength(int minLength); //string
    IRuleBuilder<T> MaxLength(int maxLength); //string
    //IRuleBuilder<T> Matches(string regex); //string
    //IRuleBuilder<T> LessThan(T value); //IComparable
    //IRuleBuilder<T> LessThanOrEqual(T value); //IComparable
    //IRuleBuilder<T> GreaterThan(T value); //IComparable
    //IRuleBuilder<T> GreaterThanOrEqual(T value); //IComparable    
    //IRuleBuilder<T> InclusiveBetween(T min, T max); //INumber
    //IRuleBuilder<T> ExclusiveBetween(T min, T max); //INumber
    //IRuleBuilder<T> PrecisionScale(int precision, int scale); //INumber
}

public interface IRuleBuilder<T> : ITypelyBuilder<T>
{
    IRuleBuilder<T> WithErrorCode(string errorCode);
    IRuleBuilder<T> WithMessage(string message);
    IRuleBuilder<T> WithMessage(Expression<Func<string>> expression);

    //IRuleBuilder<T> WithErrorCode(string errorCode);
}