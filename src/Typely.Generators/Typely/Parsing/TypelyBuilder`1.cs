using System.Linq.Expressions;
using Typely.Core;
using Typely.Generators.Logging;

namespace Typely.Generators.Typely.Parsing;

internal class TypelyBuilder<T> : ITypelyBuilder<T>
{
    protected EmittableType _emittableType;

    public TypelyBuilder(EmittableType emittableType)
    {
        Logger.Log("TypelyBuilder ctor");
        _emittableType = emittableType;
    }

    public ITypelyBuilder<T> AsClass()
    {
        Logger.Log("AsClass");
        throw new NotImplementedException();
    }

    public ITypelyBuilder<T> AsRecord()
    {
        Logger.Log("AsRecord");
        throw new NotImplementedException();
    }

    public ITypelyBuilder<T> AsStruct()
    {
        Logger.Log("AsStruct");
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> ExclusiveBetween(T min, T max)
    {
        Logger.Log($"ExclusiveBetween : {min} - {max}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> GreaterThan(T value)
    {
        Logger.Log($"GreaterThan : {value}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> GreaterThanOrEqual(T value)
    {
        Logger.Log($"GreaterThanOrEqual : {value}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> InclusiveBetween(T min, T max)
    {
        Logger.Log($"InclusiveBetween : {min} - {max}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> Length(int min, T max)
    {
        Logger.Log($"Length : {min} - {max}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> Length(int exactLength)
    {
        Logger.Log($"Length : {exactLength}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> LessThan(T value)
    {
        Logger.Log($"LessThan : {value}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> LessThanOrEqual(T value)
    {
        Logger.Log($"LessThanOrEqual : {value}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> Matches(string regex)
    {
        Logger.Log($"Matches : {regex}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> MaxLength(int maxLength)
    {
        Logger.Log($"MaxLength : {maxLength}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> MinLength(int minLength)
    {
        Logger.Log($"MinLength : {minLength}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> Must(Expression<Func<T, bool>> predicate)
    {
        Logger.Log($"Must");
        throw new NotImplementedException();
    }

    public ITypelyBuilder<T> Namespace(string value)
    {
        Logger.Log($"Namespace : {value}");
        _emittableType.Namespace = value;
        return this;
    }

    public IRuleBuilder<T> NotEmpty()
    {
        Logger.Log("NotEmpty");
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> NotEqual(T value)
    {
        Logger.Log($"NotEqual : {value}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> PrecisionScale(int precision, int scale)
    {
        Logger.Log($"PrecisionScale : {precision} - {scale}");
        throw new NotImplementedException();
    }

    public ITypelyBuilder<T> WithName(string name)
    {
        Logger.Log($"WithName : {name}");
        throw new NotImplementedException();
    }
}