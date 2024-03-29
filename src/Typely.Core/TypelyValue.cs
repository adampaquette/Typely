﻿namespace Typely.Core;

/// <summary>
/// Static methods for a type generated by Typely.
/// </summary>
public static class TypelyValue
{
    /// <summary>
    /// Validates the value object and throw if in error.
    /// </summary>
    /// <typeparam name="TValue">The underlying type of the value object.</typeparam>
    /// <typeparam name="TTypelyValue">Value object's type.</typeparam>
    /// <param name="value">Value to validate.</param>
    /// <exception cref="ValidationException"></exception>
    public static void ValidateAndThrow<TValue, TTypelyValue>(TValue value) where TTypelyValue : ITypelyValue<TValue, TTypelyValue>
    {
#if NET7_0_OR_GREATER
        var validationError = TTypelyValue.Validate(value);
        if (validationError != null)
        {
            throw new ValidationException(validationError);
        }
#else
        throw new NotImplementedException();
#endif
    }

    /// <summary>
    /// Creates a <see cref="TTypelyValue"/> if the validation succeeds.
    /// </summary>
    /// <param name="value">The underlying <see cref="TValue"/> value.</param>
    /// <returns>The created <see cref="TTypelyValue"/>.</returns>
    /// <exception cref="ArgumentException">If validations failed.</exception>
    public static TTypelyValue From<TValue, TTypelyValue>(TValue value) where TTypelyValue : ITypelyValue<TValue, TTypelyValue>
    {
#if NET7_0_OR_GREATER
        return TTypelyValue.From(value);
#else
        throw new NotImplementedException();
#endif
    }
}