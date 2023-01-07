using System.Linq.Expressions;
using Typely.Core;
using Typely.Core.Builders;
// ReSharper disable HeapView.ObjectAllocation
// ReSharper disable HeapView.BoxingAllocation
// ReSharper disable HeapView.ClosureAllocation

namespace Typely.Generators.Typely.Parsing.String;

internal class TypelyBuilderOfString : ITypelyBuilderOfString
{
    protected EmittableType EmittableType;
    protected readonly List<EmittableType> EmittableTypes;

    public TypelyBuilderOfString(EmittableType emittableType, List<EmittableType> emittableTypes)
    {
        EmittableType = emittableType;
        EmittableTypes = emittableTypes;
    }

    public ITypelyBuilderOfString For(string typeName)
    {
        EmittableType.SetTypeName(typeName);
        return this;
    }

    public ITypelyBuilderOfString WithName(string name)
    {
        EmittableType.SetName(name);
        return this;
    }

    public ITypelyBuilderOfString WithName(Expression<Func<string>> expression)
    {
        EmittableType.WithName(expression);
        return this;
    }

    public ITypelyBuilderOfString WithNamespace(string value)
    {
        EmittableType.SetNamespace(value);
        return this;
    }

    public ITypelyBuilderOfString AsStruct()
    {
        EmittableType.AsStruct();
        return this;
    }

    public ITypelyBuilderOfString AsClass()
    {
        EmittableType.AsClass();
        return this;
    }

    public IRuleBuilderOfString NotEmpty() => AddRule(
        errorCode: ErrorCodes.NotEmpty,
        rule: (x) => string.IsNullOrWhiteSpace(x),
        message: () => ErrorMessages.NotEmpty);

    public IRuleBuilderOfString NotEqual(string value) => AddRule(
        errorCode: ErrorCodes.NotEqual,
        rule: (x) => x.Equals(value),
        message: () => ErrorMessages.NotEqual,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    public IRuleBuilderOfString Length(int min, int max) => AddRule(
        errorCode: ErrorCodes.Length,
        rule: (x) => x.Length < min || x.Length > max,
        message: () => ErrorMessages.Length,
        (ValidationPlaceholders.MinLength, min), (ValidationPlaceholders.MaxLength, max));

    public IRuleBuilderOfString Length(int exactLength) => AddRule(
        errorCode: ErrorCodes.ExactLength,
        rule: (x) => x.Length != exactLength,
        message: () => ErrorMessages.ExactLength,
        placeholders: (ValidationPlaceholders.ExactLength, exactLength));

    public IRuleBuilderOfString MaxLength(int maxLength) => AddRule(
        errorCode: ErrorCodes.MaxLength,
        rule: (x) => x.Length > maxLength,
        message: () => ErrorMessages.MaxLength,
        placeholders: (ValidationPlaceholders.MaxLength, maxLength));

    public IRuleBuilderOfString MinLength(int minLength) => AddRule(
        errorCode: ErrorCodes.MinLength,
        rule: (x) => x.Length < minLength,
        message: () => ErrorMessages.MinLength,
        placeholders: (ValidationPlaceholders.MinLength, minLength));

    public IRuleBuilderOfString LessThan(string value) => AddRule(
        errorCode: ErrorCodes.LessThan,
        rule: (x) => string.Compare(x, value, StringComparison.Ordinal) >= 0,
        message: () => ErrorMessages.LessThan,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    public IRuleBuilderOfString LessThanOrEqual(string value) => AddRule(
        errorCode: ErrorCodes.LessThanOrEqualTo,
        rule: (x) => string.Compare(x, value, StringComparison.Ordinal) > 0,
        message: () => ErrorMessages.LessThanOrEqualTo,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    public IRuleBuilderOfString GreaterThan(string value) => AddRule(
        errorCode: ErrorCodes.GreaterThan,
        rule: (x) => string.Compare(x, value, StringComparison.Ordinal) <= 0,
        message: () => ErrorMessages.GreaterThan,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    public IRuleBuilderOfString GreaterThanOrEqual(string value) => AddRule(
        errorCode: ErrorCodes.GreaterThanOrEqualTo,
        rule: (x) => string.Compare(x, value, StringComparison.Ordinal) < 0,
        message: () => ErrorMessages.GreaterThanOrEqualTo,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    public IRuleBuilderOfString Must(Expression<Func<string, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    private IRuleBuilderOfString AddRule(string errorCode, Expression<Func<string, bool>> rule, 
        Expression<Func<string>> message, params (string Key, object Value)[] placeholders) =>
        AddRule(EmittableRule.From(errorCode, rule, message, placeholders));

    private IRuleBuilderOfString AddRule(EmittableRule emittableRule)
    {
        EmittableType.AddRule(emittableRule);
        return (IRuleBuilderOfString)this;
    }

    public IFactoryOfString AsFactory()
    {
        EmittableTypes.Remove(EmittableType);
        return new FactoryOfString(EmittableType, EmittableTypes);
    }
}