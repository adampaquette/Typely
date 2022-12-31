using System.Linq.Expressions;
using Typely.Core;
using Typely.Generators.Logging;

namespace Typely.Generators.Typely.Parsing;

internal class TypelyBuilder<TValue> : ITypelyBuilder<TValue>
{
    protected EmittableType _emittableType;

    public TypelyBuilder(EmittableType emittableType)
    {
        Logger.Log("TypelyBuilder ctor");
        _emittableType = emittableType;
    }

    public ITypelyBuilder<TValue> AsClass()
    {
        Logger.Log("AsClass");
        _emittableType.TypeKind = TypeKind.Class;
        return this;
    }

    public ITypelyBuilder<TValue> AsRecord()
    {
        Logger.Log("AsRecord");
        _emittableType.TypeKind = TypeKind.Record;
        return this;
    }

    public ITypelyBuilder<TValue> AsStruct()
    {
        Logger.Log("AsStruct");
        _emittableType.TypeKind = TypeKind.Struct;
        return this;
    }

    public IRuleBuilder<TValue> ExclusiveBetween(TValue min, TValue max)
    {
        Logger.Log($"ExclusiveBetween : {min} - {max}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> GreaterThan(TValue value)
    {
        Logger.Log($"GreaterThan : {value}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> GreaterThanOrEqual(TValue value)
    {
        Logger.Log($"GreaterThanOrEqual : {value}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> InclusiveBetween(TValue min, TValue max)
    {
        Logger.Log($"InclusiveBetween : {min} - {max}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> Length(int min, TValue max)
    {
        Logger.Log($"Length : {min} - {max}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> Length(int exactLength)
    {
        Logger.Log($"Length : {exactLength}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> LessThan(TValue value)
    {
        Logger.Log($"LessThan : {value}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> LessThanOrEqual(TValue value)
    {
        Logger.Log($"LessThanOrEqual : {value}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> Matches(string regex)
    {
        Logger.Log($"Matches : {regex}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> MaxLength(int maxLength)
    {
        Logger.Log($"MaxLength : {maxLength}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> MinLength(int minLength)
    {
        Logger.Log($"MinLength : {minLength}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> Must(Expression<Func<TValue, bool>> predicate)
    {
        Logger.Log($"Must");
        throw new NotImplementedException();
    }

    public ITypelyBuilder<TValue> Namespace(string value)
    {
        Logger.Log($"Namespace : {value}");
        _emittableType.Namespace = value;
        return this;
    }

    public IRuleBuilder<TValue> NotEmpty()
    {
        Logger.Log("NotEmpty");
        var validation = new EmittableValidation
        {
            ValidationExpression = (x) => x != default,
            ValidationMessage = "'{Name}' must not be empty."
        };

        _emittableType.CurrentValidation = validation;
        _emittableType.Validations.Add(validation);
        return (IRuleBuilder<TValue>)this;
    }

    public IRuleBuilder<TValue> NotEqual(TValue value)
    {
        Logger.Log($"NotEqual : {value}");
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> PrecisionScale(int precision, int scale)
    {
        Logger.Log($"PrecisionScale : {precision} - {scale}");
        throw new NotImplementedException();
    }

    public ITypelyBuilder<TValue> WithName(string name)
    {
        Logger.Log($"WithName : {name}");
        throw new NotImplementedException();
    }
}