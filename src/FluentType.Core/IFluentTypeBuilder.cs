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
    
    IRuleBuilder<T> NotEmpty();
    IRuleBuilder<T> NotEqual(T value);    
    IRuleBuilder<T> Length(int min, T max);
    IRuleBuilder<T> Length(int exactLength);
    IRuleBuilder<T> MinLength(int minLength);
    IRuleBuilder<T> MaxLength(int maxLength);
    IRuleBuilder<T> LessThan(int value);
    IRuleBuilder<T> LessThanOrEqual(int value);
    IRuleBuilder<T> GreaterThan(int value);
    IRuleBuilder<T> GreaterThanOrEqual(int value);
    IRuleBuilder<T> Must(Expression<Func<T, bool>> predicate);
    IRuleBuilder<T> Matches(string regex);
    IRuleBuilder<T> InclusiveBetween(T min, T max);
    IRuleBuilder<T> ExclusiveBetween(T min, T max);
    IRuleBuilder<T> PrecisionScale(int precision, int scale);
}

public interface IRuleBuilder<T> : IFluentTypeBuilder<T>
{
    IRuleBuilder<T> WithMessage(string message);
    IRuleBuilder<T> When(Expression<Func<T, bool>> predicate);
}