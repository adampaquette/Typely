namespace Typely.Core;

public static class ValidationErrorFactory
{
    public static ValidationError Create<TValue>(TValue value, string errorCode, string errorMessageWithPlaceholders, string source, Dictionary<string, object?> placeholderValues)
    {
        object? attemptedValue = null;

        if (TypelyOptions.Instance.IsSensitiveDataLoggingEnabled)
        {
            placeholderValues.Add("Value", value);
            attemptedValue = value;
        }

        var errorMessage = errorMessageWithPlaceholders;

        return new ValidationError(errorCode, errorMessage, errorMessageWithPlaceholders, attemptedValue, source, placeholderValues);
    }
}
