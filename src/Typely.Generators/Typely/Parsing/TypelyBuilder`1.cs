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

    public IRuleBuilder<TValue> Length(int min, int max)
    {
        if (typeof(TValue) != typeof(string))
        {
            //TODO error diagnostic
        }

        Expression<Func<string, bool>> validation = (string x) => x.Length < min || x.Length > max;

        var emittableValidation = EmittableValidation.From(ErrorCodes.Length, validation, () => ErrorMessages.Length);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.MinLength, min);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.MaxLength, max);

        return AddValidation(emittableValidation);
    }

    public IRuleBuilder<TValue> Length(int exactLength)
    {
        if (typeof(TValue) != typeof(string))
        {
            //TODO error diagnostic
        }

        Expression<Func<string, bool>> validation = (string x) => x.Length != exactLength;

        var emittableValidation = EmittableValidation.From(ErrorCodes.Length, validation, () => ErrorMessages.Length);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.ExactLength, exactLength);

        return AddValidation(emittableValidation);
    }

    public IRuleBuilder<TValue> MinLength(int minLength)
    {
        if (typeof(TValue) != typeof(string))
        {
            //TODO error diagnostic
        }

        Expression<Func<string, bool>> validation = (string x) => x.Length < minLength;

        var emittableValidation = EmittableValidation.From(ErrorCodes.MinLength, validation, () => ErrorMessages.MinLength);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.MinLength, minLength);

        return AddValidation(emittableValidation);
    }

    public IRuleBuilder<TValue> MaxLength(int maxLength)
    {
        if (typeof(TValue) != typeof(string))
        { 
            //TODO error diagnostic
        }

        Expression<Func<string, bool>> validation = (string x) => x.Length > maxLength;

        var emittableValidation = EmittableValidation.From(ErrorCodes.MaxLength, validation, () => ErrorMessages.MaxLength);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.MaxLength, maxLength);

        return AddValidation(emittableValidation);
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