namespace Typely.Core;

/// <summary>
/// Exception thrown when a validation fails.
/// </summary>
public class ValidationException : Exception
{
    public ValidationError ValidationError { get; }

    public ValidationException(ValidationError validationError) : base(validationError.ErrorMessage)
    {
        ValidationError = validationError;
    }
}