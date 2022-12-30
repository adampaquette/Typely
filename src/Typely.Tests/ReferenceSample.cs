using System.Globalization;
using Typely.Core;

namespace Typely.Tests;

public class ReferenceSample : IValue<int, ReferenceSample>
{
    public int Value { get; }

    public ReferenceSample(int value)
    {
        Value = value;
    }

    public static ReferenceSample From(int value) => new ReferenceSample(value);

    public static ValidationResult Validate(int value)
    {
        return new ValidationResult();
    }
}

public class ValidationResult
{
    /// <summary>
    /// A list of errors that occured during the validation.
    /// </summary>
    public List<ValidationError> Errors { get; } = new List<ValidationError>();
}

/// <summary>
/// Represent a single validation that resulted in an error.
/// </summary>
public class ValidationError
{
    /// <summary>
    /// A unique identifier for the error.
    /// </summary>
    /// <remarks>The value is used for translations.</remarks>
    public string ErrorCode { get; init; }

    /// <summary>
    /// The error message translated.
    /// </summary>
    public string ErrorMessage { get; init; }

    /// <summary>
    /// The error message translated with placeholders.
    /// </summary>
    public string ErrorMessageWithPlaceHolders { get; init; }

    /// <summary>
    /// The value that caused the error.
    /// </summary>
    public object AttemptedValue { get; init; }

    /// <summary>
    /// Type that generated the error.
    /// </summary>
    public string Source { get; init; }

    /// <summary>
    /// List of placeholders with their values.
    /// </summary>
    public Dictionary<string, object> PlaceholderValues { get; init; } = new Dictionary<string, object>();

    /// <summary>
    /// Constructor of <see cref="ValidationError"/>.
    /// </summary>
    /// <param name="errorCode">A unique identifier for the error.</param>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="attemptedValue">The value that caused the error.</param>
    public ValidationError(string errorCode, string errorMessage, string errorMessageWithPlaceHolders, object attemptedValue, string source)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        ErrorMessageWithPlaceHolders = errorMessageWithPlaceHolders;
        AttemptedValue = attemptedValue;
        Source = source;
    }
}

public class ValidationException : Exception
{

}