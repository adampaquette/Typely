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

    public ITypelyBuilder<TValue> AsStruct()
    {
        _emittableType.ConstructTypeKind = ConstructTypeKind.Struct;
        return this;
    }

    public ITypelyBuilder<TValue> Namespace(string value)
    {
        _emittableType.Namespace = value;
        return this;
    }

    public ITypelyBuilder<TValue> Name(string name)
    {
        _emittableType.Name = Expression.Lambda<Func<string>>(Expression.Constant(name));
        return this;
    }

    public ITypelyBuilder<TValue> Name(Expression<Func<string>> expression)
    {
        _emittableType.Name = expression;
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

    public IRuleBuilder<TValue> NotEmpty()
    {
        Expression validation = typeof(TValue) == typeof(string)
            ? (string x) => string.IsNullOrWhiteSpace(x)
            : (TValue x) => EqualityComparer<TValue>.Default.Equals(x, default!);

        var emittableValidation = EmittableValidation.From(ErrorCodes.NotEmpty, validation, () => ErrorMessages.NotEmpty);

        return AddValidation(emittableValidation);
    }

    public IRuleBuilder<TValue> NotEqual(TValue value)
    {
        Expression<Func<TValue, bool>> validation = (TValue x) => !EqualityComparer<TValue>.Default.Equals(x, value);
       
        var emittableValidation = EmittableValidation.From(ErrorCodes.NotEqual, validation, () => ErrorMessages.NotEqual);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.ComparisonValue, value);

        return AddValidation(emittableValidation);
    }

    public IRuleBuilder<TValue> PrecisionScale(int precision, int scale)
    {
        throw new NotImplementedException();
    }

    private IRuleBuilder<TValue> AddValidation(EmittableValidation emittableValidation)
    {
        _emittableType.CurrentValidation = emittableValidation;
        _emittableType.Validations.Add(emittableValidation);
        return (IRuleBuilder<TValue>)this;
    }
}