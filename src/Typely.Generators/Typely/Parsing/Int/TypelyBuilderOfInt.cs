using System.Linq.Expressions;
using Typely.Core;
using Typely.Core.Builders;
// ReSharper disable HeapView.ObjectAllocation
// ReSharper disable HeapView.BoxingAllocation
// ReSharper disable HeapView.ClosureAllocation

namespace Typely.Generators.Typely.Parsing.Int;

/// <summary>
/// Builder dedicated to creating a value object with the underlying string type.
/// </summary>
internal class TypelyBuilderOfInt : ITypelyBuilderOfInt
{
    protected EmittableType EmittableType;
    protected readonly List<EmittableType> EmittableTypes;

    public TypelyBuilderOfInt(EmittableType emittableType, List<EmittableType> emittableTypes)
    {
        EmittableType = emittableType;
        EmittableTypes = emittableTypes;
    }

    /// <inheritdoc/>
    public ITypelyBuilderOfInt For(string typeName)
    {
        EmittableType.SetTypeName(typeName);
        return this;
    }

    /// <inheritdoc/>
    public ITypelyBuilderOfInt WithName(string name)
    {
        EmittableType.SetName(name);
        return this;
    }

    /// <inheritdoc/>
    public ITypelyBuilderOfInt WithName(Expression<Func<string>> expression)
    {
        EmittableType.WithName(expression);
        return this;
    }

    /// <inheritdoc/>
    public ITypelyBuilderOfInt WithNamespace(string value)
    {
        EmittableType.SetNamespace(value);
        return this;
    }

    /// <inheritdoc/>
    public ITypelyBuilderOfInt AsStruct()
    {
        EmittableType.AsStruct();
        return this;
    }

    /// <inheritdoc/>
    public ITypelyBuilderOfInt AsClass()
    {
        EmittableType.AsClass();
        return this;
    }

    /// <inheritdoc/>
    public IRuleBuilderOfInt NotEmpty() => AddRule(
        errorCode: ErrorCodes.NotEmpty,
        rule: (x) => x == 0,
        message: () => ErrorMessages.NotEmpty);

    /// <inheritdoc/>
    public IRuleBuilderOfInt NotEqual(int value) => AddRule(
        errorCode: ErrorCodes.NotEqual,
        rule: (x) => x.Equals(value),
        message: () => ErrorMessages.NotEqual,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    /// <inheritdoc/>
    public IRuleBuilderOfInt LessThan(int value) => AddRule(
        errorCode: ErrorCodes.LessThan,
        rule: (x) => x >= value,
        message: () => ErrorMessages.LessThan,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    /// <inheritdoc/>
    public IRuleBuilderOfInt LessThanOrEqual(int value) => AddRule(
        errorCode: ErrorCodes.LessThanOrEqualTo,
        rule: (x) => x > value,
        message: () => ErrorMessages.LessThanOrEqualTo,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    /// <inheritdoc/>
    public IRuleBuilderOfInt GreaterThan(int value) => AddRule(
        errorCode: ErrorCodes.GreaterThan,
        rule: (x) => x <= value,
        message: () => ErrorMessages.GreaterThan,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    /// <inheritdoc/>
    public IRuleBuilderOfInt GreaterThanOrEqual(int value) => AddRule(
        errorCode: ErrorCodes.GreaterThanOrEqualTo,
        rule: (x) => x < value,
        message: () => ErrorMessages.GreaterThanOrEqualTo,
        placeholders: (ValidationPlaceholders.ComparisonValue, value));

    /// <inheritdoc/>
    public IRuleBuilderOfInt Must(Expression<Func<int, bool>> predicate) => AddRule(
        errorCode: ErrorCodes.Must,
        rule: predicate.Negate(),
        message: () => ErrorMessages.Must);

    private IRuleBuilderOfInt AddRule(string errorCode, Expression<Func<int, bool>> rule,
        Expression<Func<string>> message, params (string Key, object Value)[] placeholders) =>
        AddRule(EmittableRule.From(errorCode, rule, message, placeholders));

    private IRuleBuilderOfInt AddRule(EmittableRule emittableRule)
    {
        EmittableType.AddRule(emittableRule);
        return (IRuleBuilderOfInt)this;
    }

    /// <inheritdoc/>
    public IFactoryOfInt AsFactory()
    {
        EmittableTypes.Remove(EmittableType);
        return new FactoryOfInt(EmittableType, EmittableTypes);
    }
}