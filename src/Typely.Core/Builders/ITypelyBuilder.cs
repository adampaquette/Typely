using System.Linq.Expressions;

namespace Typely.Core.Builders;

/// <summary>
/// Starting point to create a value object.
/// </summary>
public interface ITypelyBuilder
{
    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying bool type.
    /// </summary>
    /// <returns>Builder of bool.</returns>
    ITypelyBuilderOfBool OfBool();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying byte type.
    /// </summary>
    /// <returns>Builder of byte.</returns>
    ITypelyBuilderOfByte OfByte();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying char type.
    /// </summary>
    /// <returns>Builder of char.</returns>
    ITypelyBuilderOfChar OfChar();

#if NET7_0_OR_GREATER
    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying DateOnly type.
    /// </summary>
    /// <returns>Builder of DateOnly.</returns>
    ITypelyBuilderOfDateOnly OfDateOnly();
#endif

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying DateTime type.
    /// </summary>
    /// <returns>Builder of DateTime.</returns>
    ITypelyBuilderOfDateTime OfDateTime();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying DateTimeOffset type.
    /// </summary>
    /// <returns>Builder of DateTimeOffset.</returns>
    ITypelyBuilderOfDateTimeOffset OfDateTimeOffset();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying int type.
    /// </summary>
    /// <returns>Builder of int.</returns>
    ITypelyBuilderOfInt OfInt();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying decimal type.
    /// </summary>
    /// <returns>Builder of decimal.</returns>
    ITypelyBuilderOfDecimal OfDecimal();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying float type.
    /// </summary>
    /// <returns>Builder of float.</returns>
    ITypelyBuilderOfFloat OfFloat();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying Guid type.
    /// </summary>
    /// <returns>Builder of Guid.</returns>
    ITypelyBuilderOfGuid OfGuid();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying double type.
    /// </summary>
    /// <returns>Builder of double.</returns>
    ITypelyBuilderOfDouble OfDouble();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying long type.
    /// </summary>
    /// <returns>Builder of long.</returns>
    ITypelyBuilderOfLong OfLong();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying sbyte type.
    /// </summary>
    /// <returns>Builder of sbyte.</returns>
    ITypelyBuilderOfSByte OfSByte();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying short type.
    /// </summary>
    /// <returns>Builder of short.</returns>
    ITypelyBuilderOfShort OfShort();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying string type.
    /// </summary>
    /// <returns>Builder of string.</returns>
    ITypelyBuilderOfString OfString();

#if NET7_0_OR_GREATER
    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying TimeOnly type.
    /// </summary>
    /// <returns>Builder of TimeOnly.</returns>
    ITypelyBuilderOfTimeOnly OfTimeOnly();
#endif

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying TimeSpan type.
    /// </summary>
    /// <returns>Builder of TimeSpan.</returns>
    ITypelyBuilderOfTimeSpan OfTimeSpan();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying uint type.
    /// </summary>
    /// <returns>Builder of uint.</returns>
    ITypelyBuilderOfUInt OfUInt();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying ulong type.
    /// </summary>
    /// <returns>Builder of ulong.</returns>
    ITypelyBuilderOfULong OfULong();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying ushort type.
    /// </summary>
    /// <returns>Builder of ushort.</returns>
    ITypelyBuilderOfUShort OfUShort();
}

/// <summary>
/// Base interface for all builders.
/// </summary>
/// <typeparam name="TBuilder">Type of the inheriting builder.</typeparam>
public interface ITypelyBuilder<TBuilder>
{
    /// <summary>
    /// Sets the type to generate.
    /// </summary>
    /// <param name="typeName">Name of the type.</param>
    /// <returns>Fluent <see cref="TBuilder"/>.</returns>
    TBuilder For(string typeName);

    /// <summary>
    /// Makes the generated type a struct.
    /// </summary>
    /// <returns>Fluent <see cref="TBuilder"/>.</returns>
    TBuilder AsStruct();

    /// <summary>
    /// Makes the generated type a class.
    /// </summary>
    /// <returns>Fluent <see cref="TBuilder"/>.</returns>
    TBuilder AsClass();

    /// <summary>
    /// Sets the namespace associated to the generate type.
    /// </summary>
    /// <param name="value">Namespace</param>
    /// <returns>Fluent <see cref="TBuilder"/>.</returns>
    TBuilder WithNamespace(string value);

    /// <summary>
    /// Sets the name used in the validation error message.
    /// </summary>
    /// <param name="name">User friendly name for the type.</param>
    /// <returns>Fluent <see cref="TBuilder"/>.</returns>
    TBuilder WithName(string name);

    /// <summary>
    /// Sets the name used in the validation error message. Supports localization.
    /// </summary>
    /// <param name="expression">An expression to be evaluated at runtime to get the name.</param>
    /// <returns>Fluent <see cref="TBuilder"/>.</returns>
    TBuilder WithName(Expression<Func<string>> expression);
}

/// <summary>
/// Commun interface for all builders.
/// </summary>
/// <typeparam name="TBuilder">Type of the inheriting builder.</typeparam>
/// <typeparam name="TRuleBuilder">Type of the inheriting rule builder.</typeparam>
/// <typeparam name="TValue">Underlying value's type.</typeparam>
/// <typeparam name="TFactory">Factory to build similar objects.</typeparam>
public interface ITypelyBuilder<TBuilder, TRuleBuilder, TValue, TFactory> : ITypelyBuilder<TBuilder>
    where TBuilder : ITypelyBuilder<TBuilder, TRuleBuilder, TValue, TFactory>
    where TRuleBuilder : IRuleBuilder<TBuilder, TRuleBuilder, TValue, TFactory>
{
    /// <summary>
    /// Creates a factory to generate similar types based on the current configuration.
    /// </summary>
    /// <returns>Fluent <see cref="TFactory"/>.</returns>
    TFactory AsFactory();

    /// <summary>
    /// Ensures the value is not empty.
    /// </summary>
    /// <returns>Fluent <see cref="TRuleBuilder"/>.</returns>
    TRuleBuilder NotEmpty();

    /// <summary>
    /// Ensures the value is not equal to the specified value.
    /// </summary>
    /// <param name="value">Value to compare to.</param>
    /// <returns>Fluent <see cref="TRuleBuilder"/>.</returns>
    TRuleBuilder NotEqual(TValue value);

    /// <summary>
    /// Ensures the value fullfill the specified expression.
    /// </summary>
    /// <param name="predicate">Expression that must be fullfilled.</param>
    /// <returns>Fluent <see cref="TRuleBuilder"/>.</returns>
    TRuleBuilder Must(Expression<Func<TValue, bool>> predicate);

    /// <summary>
    /// Normalizes the value using the specified expression.
    /// </summary>
    /// <param name="normalizer">The normalizer function.</param>
    /// <returns>Fluent <see cref="TBuilder"/>.</returns>
    TBuilder Normalize(Expression<Func<TValue, TValue>> normalizer);
}

/// <summary>
/// Common interface for all rule builders.
/// </summary>
/// <typeparam name="TBuilder">Type of the inheriting builder.</typeparam>
/// <typeparam name="TRuleBuilder">Type of the inheriting rule builder.</typeparam>
/// <typeparam name="TValue">Underlying value's type.</typeparam>
/// <typeparam name="TFactory">Factory to build similar objects.</typeparam>
public interface IRuleBuilder<TBuilder, TRuleBuilder, TValue, TFactory>
    where TBuilder : ITypelyBuilder<TBuilder, TRuleBuilder, TValue, TFactory>
    where TRuleBuilder : IRuleBuilder<TBuilder, TRuleBuilder, TValue, TFactory>
{
    /// <summary>
    /// Sets the message associated to the rule.
    /// </summary>
    /// <param name="message">Message with support for placeholders <see cref="ValidationPlaceholders"/>.</param>
    /// <returns>Fluent <see cref="TRuleBuilder"/>.</returns>
    TRuleBuilder WithMessage(string message);

    /// <summary>
    /// Sets the message associated to the rule.
    /// </summary>
    /// <param name="expression">Function to be evaluated at runtime to get the message. Supports for localization.</param>
    /// <returns>Fluent <see cref="TRuleBuilder"/>.</returns>
    TRuleBuilder WithMessage(Expression<Func<string>> expression);

    /// <summary>
    /// Sets the unique error code associated to the message.
    /// </summary>
    /// <param name="errorCode">Any error code.</param>
    /// <returns>Fluent <see cref="TRuleBuilder"/>.</returns>
    TRuleBuilder WithErrorCode(string errorCode);
}