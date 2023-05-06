namespace Typely.Core;

/// <summary>
/// Factory for ValidationError
/// </summary>
public static class ValidationErrorFactory
{
    /// <summary>
    /// Creates a ValidationError.
    /// </summary>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    /// <param name="value">Validated value.</param>
    /// <param name="errorCode">Unique code for the error.</param>
    /// <param name="errorMessageWithPlaceholders">Error message containing placeholders <see cref="ValidationPlaceholders"/> in braces.</param>
    /// <param name="typeName">The name of the calss or struct to create.</param>
    /// <param name="placeholderValues">List of the placeholders with their values.</param>
    /// <returns>A <see cref="ValidationError"/>.</returns>
    public static ValidationError Create<TValue>(TValue value, string errorCode,
        string errorMessageWithPlaceholders, string typeName, Dictionary<string, object?>? placeholderValues = null)
    {
        string? attemptedValue = null;

        if (placeholderValues == null)
        {
            placeholderValues = new Dictionary<string, object?>();
        }

        if (!placeholderValues.ContainsKey(ValidationPlaceholders.Name))
        {
            placeholderValues.Add(ValidationPlaceholders.Name, typeName);
        }

        if (typeof(TValue) == typeof(string))
        {
            var actualLength = (value as string)?.Length ?? 0;
            placeholderValues.Add(ValidationPlaceholders.ActualLength, actualLength.ToString());
        }

        // if (TypelyOptions.Instance.IsSensitiveDataLoggingEnabled)
        // {
        //     if (!placeholderValues.ContainsKey(ValidationPlaceholders.Value))
        //     {
        //         placeholderValues.Add(ValidationPlaceholders.Value, value);
        //     }
        //     attemptedValue = value;
        // }

        return new ValidationError(errorCode, errorMessageWithPlaceholders, attemptedValue, typeName, placeholderValues);
    }
}