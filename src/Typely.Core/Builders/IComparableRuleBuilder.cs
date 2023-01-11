namespace Typely.Core.Builders;

/// <summary>
/// Builder of <see cref="IComparable{T}"/> types.
/// </summary>
/// <typeparam name="TRuleBuilder">Type of the inheriting rule builder.</typeparam>
/// <typeparam name="TValue">Underlying value's type.</typeparam>
public interface IComparableRuleBuilder<TRuleBuilder, TValue>
{
    /// <summary>
    /// Ensure the value object's value is less than the specified one.
    /// </summary>
    /// <param name="value">Value to be compared to.</param>
    /// <returns>Fluent <see cref="TRuleBuilder"/>.</returns>
    TRuleBuilder LessThan(TValue value);

    /// <summary>
    /// Ensure the value object's value is less than or equal to the specified one.
    /// </summary>
    /// <param name="value">Value to be compared to.</param>
    /// <returns>Fluent <see cref="TRuleBuilder"/>.</returns>
    TRuleBuilder LessThanOrEqual(TValue value);

    /// <summary>
    /// Ensure the value object's value is greater than the specified one.
    /// </summary>
    /// <param name="value">Value to be compared to.</param>
    /// <returns>Fluent <see cref="TRuleBuilder"/>.</returns>
    TRuleBuilder GreaterThan(TValue value);

    /// <summary>
    /// Ensure the value object's value is greater than or equal to the specified one.
    /// </summary>
    /// <param name="value">Value to be compared to.</param>
    /// <returns>Fluent <see cref="TRuleBuilder"/>.</returns>
    TRuleBuilder GreaterThanOrEqual(TValue value);
}