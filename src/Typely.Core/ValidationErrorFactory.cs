namespace Typely.Core;

public static class ValidationErrorFactory
{
    public static ValidationError Create<TValue>(TValue value, string errorCode, 
        string errorMessageWithPlaceholders, string typeName, Dictionary<string, object?>? placeholderValues = null)
    {
        object? attemptedValue = null;

        if (placeholderValues == null)
        {
            placeholderValues = new Dictionary<string, object?>();
        }

        if (!placeholderValues.ContainsKey(ValidationPlaceholders.Name))
        {
            placeholderValues.Add(ValidationPlaceholders.Name, typeName);
        }

        if(typeof(TValue) == typeof(string))
        {
            var actualLength = (value as string)?.Length ?? 0;
            placeholderValues.Add(ValidationPlaceholders.ActualLength, actualLength);
        }

        if (TypelyOptions.Instance.IsSensitiveDataLoggingEnabled)
        {
            if (!placeholderValues.ContainsKey(ValidationPlaceholders.Value))
            {
                placeholderValues.Add(ValidationPlaceholders.Value, value);
            }
            attemptedValue = value;
        }

        var errorMessage = string.Format(errorMessageWithPlaceholders, placeholderValues.Values);

        return new ValidationError(errorCode, errorMessage, errorMessageWithPlaceholders, attemptedValue, typeName, placeholderValues);
    }
}
