using System.Linq.Expressions;

namespace FluentType.Core;

public interface IFluentTypeBuilder
{
    IFluentTypeBuilder<T> For<T>(string typeName);
}

public interface IFluentTypeBuilder<T>
{
    IFluentTypeBuilder<T> Namespace(string value);
    IFluentTypeBuilder<T> AsStruct();
    IFluentTypeBuilder<T> AsClass();
    IFluentTypeBuilder<T> AsRecord();
    IFluentTypeBuilder<T> WithName(string message);

    IRuleBuilder<T> NotEmpty(); //T
    IRuleBuilder<T> NotEqual(T value); //T
    IRuleBuilder<T> Must(Expression<Func<T, bool>> predicate); //T
    IRuleBuilder<T> Length(int min, T max); //string
    IRuleBuilder<T> Length(int exactLength); //string
    IRuleBuilder<T> MinLength(int minLength); //string
    IRuleBuilder<T> MaxLength(int maxLength); //string
    IRuleBuilder<T> Matches(string regex); //string
    IRuleBuilder<T> LessThan(T value); //IComparable
    IRuleBuilder<T> LessThanOrEqual(T value); //IComparable
    IRuleBuilder<T> GreaterThan(T value); //IComparable
    IRuleBuilder<T> GreaterThanOrEqual(T value); //IComparable    
    IRuleBuilder<T> InclusiveBetween(T min, T max); //INumber
    IRuleBuilder<T> ExclusiveBetween(T min, T max); //INumber
    IRuleBuilder<T> PrecisionScale(int precision, int scale); //INumber
}

public interface IRuleBuilder<T> : IFluentTypeBuilder<T>
{
    IRuleBuilder<T> WithMessage(string message);
    IRuleBuilder<T> When(Expression<Func<T, bool>> predicate);
}