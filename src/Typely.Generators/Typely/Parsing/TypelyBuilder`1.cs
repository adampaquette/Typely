using System.Linq.Expressions;
using Typely.Core;

namespace Typely.Generators.Typely.Parsing;

internal class TypelyBuilder<T> : ITypelyBuilder<T>
{
    protected EmittableType _emittableType;

    public TypelyBuilder(EmittableType emittableType)
    {
        _emittableType = emittableType;
    }

    public ITypelyBuilder<T> AsClass()
    {
        throw new NotImplementedException();
    }

    public ITypelyBuilder<T> AsRecord()
    {
        throw new NotImplementedException();
    }

    public ITypelyBuilder<T> AsStruct()
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> ExclusiveBetween(T min, T max)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> GreaterThan(T value)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> GreaterThanOrEqual(T value)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> InclusiveBetween(T min, T max)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> Length(int min, T max)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> Length(int exactLength)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> LessThan(T value)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> LessThanOrEqual(T value)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> Matches(string regex)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> MaxLength(int maxLength)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> MinLength(int minLength)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> Must(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public ITypelyBuilder<T> Namespace(string value)
    {
        _emittableType.Namespace = value;
        return this;
    }

    public IRuleBuilder<T> NotEmpty()
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> NotEqual(T value)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> PrecisionScale(int precision, int scale)
    {
        throw new NotImplementedException();
    }

    public ITypelyBuilder<T> WithName(string message)
    {
        throw new NotImplementedException();
    }
}