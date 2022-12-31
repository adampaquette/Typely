using System.Linq.Expressions;
using Typely.Core;

namespace Typely.Generators.Typely.Parsing;

internal class TypelyBuilder<TValue> : ITypelyBuilder<TValue>
{
    protected EmittableType _emittableType;

    public TypelyBuilder(EmittableType emittableType)
    {
        _emittableType = emittableType;
    }

    public ITypelyBuilder<TValue> AsClass()
    {
        _emittableType.ConstructTypeKind = ConstructTypeKind.Class;
        return this;
    }

    public ITypelyBuilder<TValue> AsRecord()
    {
        _emittableType.ConstructTypeKind = ConstructTypeKind.Record;
        return this;
    }

    public ITypelyBuilder<TValue> AsStruct()
    {
        _emittableType.ConstructTypeKind = ConstructTypeKind.Struct;
        return this;
    }

    public IRuleBuilder<TValue> ExclusiveBetween(TValue min, TValue max)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> GreaterThan(TValue value)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> GreaterThanOrEqual(TValue value)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> InclusiveBetween(TValue min, TValue max)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> Length(int min, TValue max)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> Length(int exactLength)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> LessThan(TValue value)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> LessThanOrEqual(TValue value)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> Matches(string regex)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> MaxLength(int maxLength)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> MinLength(int minLength)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> Must(Expression<Func<TValue, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public ITypelyBuilder<TValue> Namespace(string value)
    {
        _emittableType.Namespace = value;
        return this;
    }

    public IRuleBuilder<TValue> NotEmpty()
    {
        var type = typeof(TValue);

        Expression predicate = type == typeof(string)
            ? (string x) => string.IsNullOrWhiteSpace(x)
            : type.IsValueType
                ? (TValue x) => !EqualityComparer<TValue>.Default.Equals(x, default!)
                : (TValue x) => x == null || !EqualityComparer<TValue>.Default.Equals(x, default!);

        var validation = new EmittableValidation
        {
            ValidationExpression = predicate,
            ValidationMessage = "'{Name}' must not be empty."
        };

        _emittableType.CurrentValidation = validation;
        _emittableType.Validations.Add(validation);
        return (IRuleBuilder<TValue>)this;
    }

    public IRuleBuilder<TValue> NotEqual(TValue value)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<TValue> PrecisionScale(int precision, int scale)
    {
        throw new NotImplementedException();
    }

    public ITypelyBuilder<TValue> WithName(string name)
    {
        throw new NotImplementedException();
    }
}