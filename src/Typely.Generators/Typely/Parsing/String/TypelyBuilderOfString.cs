using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Typely.Core;
using Typely.Core.Builders;
// ReSharper disable HeapView.ObjectAllocation
// ReSharper disable HeapView.BoxingAllocation
// ReSharper disable HeapView.ClosureAllocation

namespace Typely.Generators.Typely.Parsing.String;

/// <summary>
/// Builder dedicated to creating a value object with the underlying string type.
/// </summary>
internal class TypelyBuilderOfString : ITypelyBuilderOfString
{
    protected EmittableType EmittableType;
    protected readonly List<EmittableType> EmittableTypes;

    public TypelyBuilderOfString(EmittableType emittableType, List<EmittableType> emittableTypes)
    {
        EmittableType = emittableType;
        EmittableTypes = emittableTypes;
    }

    /// <inheritdoc/>
    public ITypelyBuilderOfString For(string typeName)
    {
        EmittableType.SetTypeName(typeName);
        return this;
    }

    /// <inheritdoc/>
    public ITypelyBuilderOfString WithName(string name)
    {
        EmittableType.SetName(name);
        return this;
    }

    /// <inheritdoc/>
    public ITypelyBuilderOfString WithName(Expression<Func<string>> expression)
    {
        EmittableType.WithName(expression);
        return this;
    }

    /// <inheritdoc/>
    public ITypelyBuilderOfString WithNamespace(string value)
    {
        EmittableType.SetNamespace(value);
        return this;
    }

    /// <inheritdoc/>
    public ITypelyBuilderOfString AsStruct()
    {
        EmittableType.AsStruct();
        return this;
    }

    /// <inheritdoc/>
    public ITypelyBuilderOfString AsClass()
    {
        EmittableType.AsClass();
        return this;
    }

    /// <inheritdoc/>
    public IRuleBuilderOfString NotEmpty() => AddRule(
        errorCode: ErrorCodes.NotEmpty,
        rule: (x) => string.IsNullOrWhiteSpace(x),
        message: () => ErrorMessages.NotEmpty);

    /// <inheritdoc/>
    public IRuleBuilderOfString NotEqual(string value) => AddRule(
        errorCode: ErrorCodes.NotEqual,
        rule: (x) => x.Equals(value),
        message: () => ErrorMessages.NotEqual,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    /// <inheritdoc/>
    public IRuleBuilderOfString Length(int min, int max) => AddRule(
        errorCode: ErrorCodes.Length,
        rule: (x) => x.Length < min || x.Length > max,
        message: () => ErrorMessages.Length,
        (ValidationPlaceholders.MinLength, min), (ValidationPlaceholders.MaxLength, max));

    /// <inheritdoc/>
    public IRuleBuilderOfString Length(int exactLength) => AddRule(
        errorCode: ErrorCodes.ExactLength,
        rule: (x) => x.Length != exactLength,
        message: () => ErrorMessages.ExactLength,
        placeholders: (ValidationPlaceholders.ExactLength, exactLength));

    /// <inheritdoc/>
    public IRuleBuilderOfString MaxLength(int maxLength) => AddRule(
        errorCode: ErrorCodes.MaxLength,
        rule: (x) => x.Length > maxLength,
        message: () => ErrorMessages.MaxLength,
        placeholders: (ValidationPlaceholders.MaxLength, maxLength));

    /// <inheritdoc/>
    public IRuleBuilderOfString MinLength(int minLength) => AddRule(
        errorCode: ErrorCodes.MinLength,
        rule: (x) => x.Length < minLength,
        message: () => ErrorMessages.MinLength,
        placeholders: (ValidationPlaceholders.MinLength, minLength));

    /// <inheritdoc/>
    public IRuleBuilderOfString LessThan(string value) => AddRule(
        errorCode: ErrorCodes.LessThan,
        rule: (x) => string.Compare(x, value, StringComparison.Ordinal) >= 0,
        message: () => ErrorMessages.LessThan,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    /// <inheritdoc/>
    public IRuleBuilderOfString LessThanOrEqual(string value) => AddRule(
        errorCode: ErrorCodes.LessThanOrEqualTo,
        rule: (x) => string.Compare(x, value, StringComparison.Ordinal) > 0,
        message: () => ErrorMessages.LessThanOrEqualTo,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    /// <inheritdoc/>
    public IRuleBuilderOfString GreaterThan(string value) => AddRule(
        errorCode: ErrorCodes.GreaterThan,
        rule: (x) => string.Compare(x, value, StringComparison.Ordinal) <= 0,
        message: () => ErrorMessages.GreaterThan,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    /// <inheritdoc/>
    public IRuleBuilderOfString GreaterThanOrEqual(string value) => AddRule(
        errorCode: ErrorCodes.GreaterThanOrEqualTo,
        rule: (x) => string.Compare(x, value, StringComparison.Ordinal) < 0,
        message: () => ErrorMessages.GreaterThanOrEqualTo,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    /// <inheritdoc/>
    public IRuleBuilderOfString Must(Expression<Func<string, bool>> predicate) => AddRule(
        errorCode: ErrorCodes.Must,
        rule: predicate,
        message: () => ErrorMessages.Must);

    /// <inheritdoc/>
    public IRuleBuilderOfString Matches(Regex regex) => AddRule(
        errorCode: ErrorCodes.Matches,
        rule: (x) => !regex.IsMatch(x),
        message: () => ErrorMessages.Matches,
        placeholders: (ValidationPlaceholders.RegularExpression, regex.ToString()));

    private IRuleBuilderOfString AddRule(string errorCode, Expression<Func<string, bool>> rule,
        Expression<Func<string>> message, params (string Key, object Value)[] placeholders) =>
        AddRule(EmittableRule.From(errorCode, rule, message, placeholders));

    private IRuleBuilderOfString AddRule(EmittableRule emittableRule)
    {
        EmittableType.AddRule(emittableRule);
        return (IRuleBuilderOfString)this;
    }

    /// <inheritdoc/>
    public IFactoryOfString AsFactory()
    {
        EmittableTypes.Remove(EmittableType);
        return new FactoryOfString(EmittableType, EmittableTypes);
    }
}