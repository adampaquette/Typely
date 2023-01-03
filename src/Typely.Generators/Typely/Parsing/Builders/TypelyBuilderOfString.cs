using System.Linq.Expressions;
using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing.Builders;

internal class TypelyBuilderOfString : ITypelyBuilderOfString
{
    protected EmittableType _emittableType;

    public TypelyBuilderOfString(EmittableType emittableType)
    {
        _emittableType = emittableType;
    }

    public ITypelyBuilderOfString For(string typeName)
    {
        _emittableType.TypeName = typeName;
        if (_emittableType.Name == null)
        {
            _emittableType.Name = Expression.Lambda<Func<string>>(Expression.Constant(typeName));
        }
        return this;
    }

    public ITypelyBuilderOfString Name(string name)
    {
        _emittableType.Name = Expression.Lambda<Func<string>>(Expression.Constant(name));
        return this;
    }

    public ITypelyBuilderOfString Name(Expression<Func<string>> expression)
    {
        _emittableType.Name = expression;
        return this;
    }

    public ITypelyBuilderOfString Namespace(string value)
    {
        _emittableType.Namespace = value;
        return this;
    }

    public ITypelyBuilderOfString AsStruct()
    {
        _emittableType.ConstructTypeKind = ConstructTypeKind.Struct;
        return this;
    }

    public IRuleBuilderOfString NotEmpty() => AddRule(
        errorCode: ErrorCodes.NotEmpty,
        rule: (string x) => string.IsNullOrWhiteSpace(x),
        message: () => ErrorMessages.NotEmpty);

    public IRuleBuilderOfString NotEqual(string value) => AddRule(
        errorCode: ErrorCodes.NotEqual,
        rule: (string x) => x.Equals(value),
        message: () => ErrorMessages.NotEqual,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    public IRuleBuilderOfString Length(int min, int max) => AddRule(
        errorCode: ErrorCodes.ExactLength,
        rule: (string x) => x.Length < min || x.Length > max,
        message: () => ErrorMessages.LessThan,
        (ValidationPlaceholders.MinLength, min), (ValidationPlaceholders.MaxLength, max));

    public IRuleBuilderOfString Length(int exactLength) => AddRule(
        errorCode: ErrorCodes.Length,
        rule: (string x) => x.Length != exactLength,
        message: () => ErrorMessages.LessThan,
        placeholders: (ValidationPlaceholders.ExactLength, exactLength));

    public IRuleBuilderOfString MaxLength(int maxLength) => AddRule(
        errorCode: ErrorCodes.MaxLength,
        rule: (string x) => x.Length > maxLength,
        message: () => ErrorMessages.MaxLength,
        placeholders: (ValidationPlaceholders.MaxLength, maxLength));

    public IRuleBuilderOfString MinLength(int minLength) => AddRule(
        errorCode: ErrorCodes.MinLength,
        rule: (string x) => x.Length < minLength,
        message: () => ErrorMessages.MinLength,
        placeholders: (ValidationPlaceholders.MinLength, minLength));

    public IRuleBuilderOfString LessThan(string value) => AddRule(
        errorCode: ErrorCodes.LessThan,
        rule: (string x) => x.CompareTo(value) < 0,
        message: () => ErrorMessages.LessThan,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    public IRuleBuilderOfString LessThanOrEqual(string value) => AddRule(
        errorCode: ErrorCodes.LessThanOrEqualTo,
        rule: (string x) => x.CompareTo(value) <= 0,
        message: () => ErrorMessages.LessThanOrEqualTo,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    public IRuleBuilderOfString GreaterThan(string value) => AddRule(
        errorCode: ErrorCodes.GreaterThan,
        rule: (string x) => x.CompareTo(value) > 0,
        message: () => ErrorMessages.GreaterThan,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    public IRuleBuilderOfString GreaterThanOrEqual(string value) => AddRule(
        errorCode: ErrorCodes.GreaterThanOrEqualTo,
        rule: (string x) => x.CompareTo(value) >= 0,
        message: () => ErrorMessages.GreaterThanOrEqualTo,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    public IRuleBuilderOfString Must(Expression<Func<string, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    private IRuleBuilderOfString AddRule(string errorCode, Expression<Func<string, bool>> rule, Expression<Func<string>> message, params (string Key, object Value)[] placeholders) =>
        AddRule(EmittableRule.From(errorCode, rule, message, placeholders));

    private IRuleBuilderOfString AddRule(EmittableRule emittableValidation)
    {
        _emittableType.CurrentRule = emittableValidation;
        _emittableType.Rules.Add(emittableValidation);
        return (IRuleBuilderOfString)this;
    }
}
