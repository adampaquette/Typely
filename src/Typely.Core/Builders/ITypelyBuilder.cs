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
    ITypelyBuilderOfInt Int();

    /// <summary>
    /// Returns a builder dedicated to creating a value object with the underlying string type.
    /// </summary>
    /// <returns>Builder of string.</returns>
    ITypelyBuilderOfString OfString();
}

/// <summary>
/// Commun interface for all builders.
/// </summary>
/// <typeparam name="TBuilder">Type of the inheriting builder.</typeparam>
/// <typeparam name="TRuleBuilder">Type of the inheriting rule builder.</typeparam>
/// <typeparam name="TValue">Underlying value's type.</typeparam>
/// <typeparam name="TFactory">Factory to build similar objects.</typeparam>
public interface ITypelyBuilder<TBuilder, TRuleBuilder, TValue, TFactory>
    where TBuilder : ITypelyBuilder<TBuilder, TRuleBuilder, TValue, TFactory>
    where TRuleBuilder : IRuleBuilder<TBuilder, TRuleBuilder, TValue, TFactory>
{
    TBuilder For(string typeName);
    TBuilder AsStruct();
    TBuilder AsClass();
    TBuilder WithNamespace(string value);
    TBuilder WithName(string name);
    TBuilder WithName(Expression<Func<string>> expression);
    TFactory AsFactory();

    TRuleBuilder NotEmpty();
    TRuleBuilder NotEqual(TValue value);
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
    TRuleBuilder WithMessage(string message);
    TRuleBuilder WithMessage(Expression<Func<string>> expression);
    TRuleBuilder WithErrorCode(string errorCode);
}