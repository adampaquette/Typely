using System.Linq.Expressions;
using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing;

internal class TypelyBuilder<TValue> : ITypelyBuilder<TValue, RuleBuilder<TValue>, TypelyBuilder<TValue>>
{
    protected EmittableType _emittableType;

    public TypelyBuilder(EmittableType emittableType)
    {
        _emittableType = emittableType;
    }

    public TypelyBuilder<TValue> For(string typeName)
    {
        _emittableType.TypeName = typeName;
        if (_emittableType.Name == null)
        {
            _emittableType.Name = Expression.Lambda<Func<string>>(Expression.Constant(typeName));
        }
        return this;
    }

    public TypelyBuilder<TValue> AsClass()
    {
        _emittableType.ConstructTypeKind = ConstructTypeKind.Class;
        return this;
    }

    public TypelyBuilder<TValue> AsStruct()
    {
        _emittableType.ConstructTypeKind = ConstructTypeKind.Struct;
        return this;
    }

    public TypelyBuilder<TValue> Namespace(string value)
    {
        _emittableType.Namespace = value;
        return this;
    }

    public TypelyBuilder<TValue> Name(string name)
    {
        _emittableType.Name = Expression.Lambda<Func<string>>(Expression.Constant(name));
        return this;
    }

    public TypelyBuilder<TValue> Name(Expression<Func<string>> expression)
    {
        _emittableType.Name = expression;
        return this;
    }

    public RuleBuilder<TValue> InclusiveBetween(int min, int max)
    {
        Expression<Func<int, bool>> validation = (int x) => x >= min && x <= max;

        var emittableValidation = EmittableValidation.From(ErrorCodes.InclusiveBetween, validation, () => ErrorMessages.InclusiveBetween);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.Min, min);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.Max, max);

        return AddValidation(emittableValidation);
    }

    public RuleBuilder<TValue> ExclusiveBetween(int min, int max)
    {
        Expression<Func<int, bool>> validation = (int x) => x > min && x < max;

        var emittableValidation = EmittableValidation.From(ErrorCodes.ExclusiveBetween, validation, () => ErrorMessages.ExclusiveBetween);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.Min, min);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.Max, max);

        return AddValidation(emittableValidation);
    }

    public RuleBuilder<TValue> LessThan(TValue value)
    {
        Expression<Func<IComparable<TValue>, bool>> validation = (IComparable<TValue> x) => x.CompareTo(value) < 0;

        var emittableValidation = EmittableValidation.From(ErrorCodes.LessThan, validation, () => ErrorMessages.LessThan);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.ComparisonValue, value);

        return AddValidation(emittableValidation);
    }

    public RuleBuilder<TValue> LessThanOrEqual(TValue value)
    {
        Expression<Func<IComparable<TValue>, bool>> validation = (IComparable<TValue> x) => x.CompareTo(value) <= 0;

        var emittableValidation = EmittableValidation.From(ErrorCodes.LessThanOrEqualTo, validation, () => ErrorMessages.LessThanOrEqualTo);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.ComparisonValue, value);

        return AddValidation(emittableValidation);
    }

    public RuleBuilder<TValue> GreaterThan(TValue value)
    {
        Expression<Func<IComparable<TValue>, bool>> validation = (IComparable<TValue> x) => x.CompareTo(value) > 0;

        var emittableValidation = EmittableValidation.From(ErrorCodes.GreaterThan, validation, () => ErrorMessages.GreaterThan);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.ComparisonValue, value);

        return AddValidation(emittableValidation);
    }

    public RuleBuilder<TValue> GreaterThanOrEqual(TValue value)
    {
        Expression<Func<IComparable<TValue>, bool>> validation = (IComparable<TValue> x) => x.CompareTo(value) >= 0;

        var emittableValidation = EmittableValidation.From(ErrorCodes.GreaterThanOrEqualTo, validation, () => ErrorMessages.GreaterThanOrEqualTo);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.ComparisonValue, value);

        return AddValidation(emittableValidation);
    }

    public RuleBuilder<TValue> Length(int min, int max)
    {
        Expression<Func<string, bool>> validation = (string x) => x.Length < min || x.Length > max;

        var emittableValidation = EmittableValidation.From(ErrorCodes.Length, validation, () => ErrorMessages.Length);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.MinLength, min);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.MaxLength, max);

        return AddValidation(emittableValidation);
    }

    public RuleBuilder<TValue> Length(int exactLength)
    {
        Expression<Func<string, bool>> validation = (string x) => x.Length != exactLength;

        var emittableValidation = EmittableValidation.From(ErrorCodes.Length, validation, () => ErrorMessages.Length);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.ExactLength, exactLength);

        return AddValidation(emittableValidation);
    }

    public RuleBuilder<TValue> MinLength(int minLength)
    {
        Expression<Func<string, bool>> validation = (string x) => x.Length < minLength;

        var emittableValidation = EmittableValidation.From(ErrorCodes.MinLength, validation, () => ErrorMessages.MinLength);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.MinLength, minLength);

        return AddValidation(emittableValidation);
    }

    public RuleBuilder<TValue> MaxLength(int maxLength)
    {
        Expression<Func<string, bool>> validation = (string x) => x.Length > maxLength;

        var emittableValidation = EmittableValidation.From(ErrorCodes.MaxLength, validation, () => ErrorMessages.MaxLength);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.MaxLength, maxLength);

        return AddValidation(emittableValidation);
    }

    public RuleBuilder<TValue> Matches(string regex)
    {
        throw new NotImplementedException();
    }

    public RuleBuilder<TValue> Must(Expression<Func<TValue, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public RuleBuilder<TValue> NotEmpty()
    {
        Expression validation = typeof(TValue) == typeof(string)
            ? (string x) => string.IsNullOrWhiteSpace(x)
            : (TValue x) => EqualityComparer<TValue>.Default.Equals(x, default!);

        var emittableValidation = EmittableValidation.From(ErrorCodes.NotEmpty, validation, () => ErrorMessages.NotEmpty);

        return AddValidation(emittableValidation);
    }

    public RuleBuilder<TValue> NotEqual(TValue value)
    {
        Expression<Func<TValue, bool>> validation = (TValue x) => !EqualityComparer<TValue>.Default.Equals(x, value);
       
        var emittableValidation = EmittableValidation.From(ErrorCodes.NotEqual, validation, () => ErrorMessages.NotEqual);
        emittableValidation.PlaceholderValues.Add(ValidationPlaceholders.ComparisonValue, value);

        return AddValidation(emittableValidation);
    }

    public RuleBuilder<TValue> PrecisionScale(int precision, int scale)
    {
        throw new NotImplementedException();
    }

    private RuleBuilder<TValue> AddValidation(EmittableValidation emittableValidation)
    {
        _emittableType.CurrentValidation = emittableValidation;
        _emittableType.Validations.Add(emittableValidation);
        return (RuleBuilder<TValue>)this;
    }
}