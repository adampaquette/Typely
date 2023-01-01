using System.Linq.Expressions;

namespace Typely.Generators.Typely.Parsing;

/// <summary>
/// Contains the metadata of a validation to be emitted.
/// </summary>
internal class EmittableValidation
{
    /// <summary>
    /// Defines the error code associated to the message.
    /// </summary>
    public string ErrorCode { get; set; } 

    /// <summary>
    /// A validation over the underlying value to emit.
    /// </summary>
    public Expression Validation { get; }

    /// <summary>
    /// An error message to return when the validation fails.
    /// </summary>
    public Expression<Func<string>> ValidationMessage { get; set; }

    /// <summary>
    /// Contains the list of variables and values that can be formatted into the error message.
    /// </summary>
    public Dictionary<string, object?> PlaceholderValues { get; set; } = new Dictionary<string, object?>();

    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name="errorCode">The error code of the message.</param>
    /// <param name="validation">The validation to be converted to C# code.</param>
    /// <param name="validationMessage">A message to be converted to C# code.</param>
    private EmittableValidation(string errorCode, Expression validation, Expression<Func<string>> validationMessage)
    {
        ErrorCode = errorCode;
        Validation = validation;
        ValidationMessage = validationMessage;
    }

    /// <summary>
    /// Creates a <see cref="EmittableValidation"/> with the default values that can be overridden.
    /// </summary>
    /// <param name="errorCode">The error code of the message.</param>
    /// <param name="validation">The validation to be converted to C# code.</param>
    /// <param name="validationMessage">A message to be converted to C# code.</param>
    /// <returns>A <see cref="EmittableValidation"/></returns>
    public static EmittableValidation From(string errorCode, Expression validation, Expression<Func<string>> validationMessage) =>
        new EmittableValidation(errorCode, validation, validationMessage);

    /// <summary>
    /// Creates a <see cref="EmittableValidation"/> with the default values that can be overridden.
    /// </summary>
    /// <param name="errorCode">The error code of the message.</param>
    /// <param name="validation">The validation to be converted to C# code.</param>
    /// <param name="validationMessage">A message to be converted to C# code.</param>
    /// <returns>A <see cref="EmittableValidation"/></returns>
    /// <remarks>It replaces variable with constants inside the validation.</remarks>
    public static EmittableValidation From<TDelegate>(string errorCode, Expression<TDelegate> validation, Expression<Func<string>> validationMessage) =>
        new EmittableValidation(errorCode, validation.ReplaceVariablesWithConstants(), validationMessage);
}