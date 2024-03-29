﻿namespace Typely.Core;

/// <summary>
/// Represents a type having an underlying value.
/// </summary>
/// <typeparam name="TValue">The type of the underlying value.</typeparam>
public interface ITypelyValue<out TValue>
{
    /// <summary>
    /// The underlying value.
    /// </summary>
    TValue Value { get; }
}

/// <summary>
/// Represents a type that can only be created if validations pass for the underlying value.
/// </summary>
/// <typeparam name="TValue">The type of the underlying value.</typeparam>
/// <typeparam name="TTypelyValue">Type inheriting from this interface.</typeparam>
public interface ITypelyValue<TValue, TTypelyValue> : ITypelyValue<TValue> where TTypelyValue : ITypelyValue<TValue, TTypelyValue>
{
#if NET7_0_OR_GREATER
    /// <summary>
    /// Validates the values.
    /// </summary>
    /// <param name="value">Value to be validated.</param>
    /// <returns>Returns a <see cref="ValidationError"/> if one of the validations failed otherwise <see langword="null"/>.</returns>
    static abstract ValidationError? Validate(TValue value);

    /// <summary>
    /// Creates a <see cref="TTypelyValue"/> if the validation succeeds.
    /// </summary>
    /// <param name="value">The underlying <see cref="TValue"/> value.</param>
    /// <returns>The created <see cref="TTypelyValue"/>.</returns>
    /// <exception cref="ArgumentException">If validations failed.</exception>
    static abstract TTypelyValue From(TValue value);

    /// <summary>
    /// Tries to create a <see cref="TTypelyValue"/> by validating the value first.
    /// </summary>
    /// <param name="value">The underlying <see cref="TValue"/> value.</param>
    /// <param name="typelyType">The created <see cref="TTypelyValue"/>.</param>
    /// <param name="validationError">A localized error.</param>
    /// <returns><see langword="true" /> if the type has been created without errors.</returns>
    static abstract bool TryFrom(TValue value, out TTypelyValue? typelyType, out ValidationError? validationError);
#endif
}