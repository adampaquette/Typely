namespace Typely.Core;

/// <summary>
/// Represent a single validation that resulted in an error.
/// </summary>
public class ValidationError
{
    /// <summary>
    /// A unique identifier for the error.
    /// </summary>
    /// <remarks>The value is used for translations.</remarks>
    public string ErrorCode { get; }

    /// <summary>
    /// The error message localized.
    /// </summary>
    public string ErrorMessage { get; }

    /// <summary>
    /// The error message localized with placeholders.
    /// </summary>
    public string ErrorMessageWithPlaceholders { get; }

    /// <summary>
    /// The value that caused the error.
    /// </summary>
    public object? AttemptedValue { get; }

    /// <summary>
    /// Type that generated the error.
    /// </summary>
    public string Source { get; }

    /// <summary>
    /// List of placeholders with their values.
    /// </summary>
    public Dictionary<string, object?> PlaceholderValues { get; }

    /// <summary>
    /// Constructor of <see cref="ValidationError"/>.
    /// </summary>
    /// <param name="errorCode">A unique identifier for the error.</param>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="attemptedValue">The value that caused the error.</param>
    public ValidationError(string errorCode, string errorMessage, string errorMessageWithPlaceholders, object? attemptedValue, string source, Dictionary<string, object?> placeholderValues)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        ErrorMessageWithPlaceholders = errorMessageWithPlaceholders;
        AttemptedValue = attemptedValue;
        Source = source;
        PlaceholderValues = placeholderValues;
    }
}