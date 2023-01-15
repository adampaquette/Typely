using System.Linq.Expressions;

namespace Typely.Core.Builders;

/// <summary>
/// Starting point to create a value object.
/// </summary>
public interface ITypelyBuilder
{
    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying int type.
    /// </summary>
    /// <returns>Builder of int.</returns>
    ITypelyBuilderOfInt OfInt();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying string type.
    /// </summary>
    /// <returns>Builder of string.</returns>
    ITypelyBuilderOfString OfString();
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
}

/// <summary>
/// Commun interface for all rule builders.
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