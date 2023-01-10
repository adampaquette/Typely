namespace Typely.Core.Builders;

/// <summary>
/// Builder of <see cref="IComparable{T}"/> types.
/// </summary>
/// <typeparam name="TRuleBuilder">Type of the inheriting rule builder.</typeparam>
/// <typeparam name="TValue">Underlying value's type.</typeparam>
public interface IComparableRuleBuilder<TRuleBuilder, TValue>
{
    TRuleBuilder LessThan(TValue value);
    TRuleBuilder LessThanOrEqual(TValue value);
    TRuleBuilder GreaterThan(TValue value);
    TRuleBuilder GreaterThanOrEqual(TValue value);
}