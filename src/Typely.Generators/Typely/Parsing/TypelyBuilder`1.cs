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

        Expression validation = type == typeof(string)
            ? (string x) => string.IsNullOrWhiteSpace(x)
            : (TValue x) => !EqualityComparer<TValue>.Default.Equals(x, default!);

        var emittableValidation = new EmittableValidation(ErrorCodes.NotEmpty, validation, () => ErrorMessages.NotEmpty);

        _emittableType.CurrentValidation = emittableValidation;
        _emittableType.Validations.Add(emittableValidation);
        return (IRuleBuilder<TValue>)this;
    }

    public IRuleBuilder<TValue> NotEqual(TValue value)
    {
        var type = typeof(TValue);

        Expression validation = (TValue x) => !EqualityComparer<TValue>.Default.Equals(x, value);                

        var emittableValidation = new EmittableValidation(ErrorCodes.NotEqual, validation, () => ErrorMessages.NotEqual);

        _emittableType.CurrentValidation = emittableValidation;
        _emittableType.Validations.Add(emittableValidation);
        return (IRuleBuilder<TValue>)this;
    }

    public IRuleBuilder<TValue> PrecisionScale(int precision, int scale)
    {
        throw new NotImplementedException();
    }

    public ITypelyBuilder<TValue> Name(string name)
    {
        _emittableType.Name = name;
        return this;
    }
}