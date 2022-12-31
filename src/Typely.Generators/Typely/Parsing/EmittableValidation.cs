using System.Linq.Expressions;

namespace Typely.Generators.Typely.Parsing;

internal class EmittableValidation
{
    public Expression Validation { get; }
    public string ErrorCode { get; set; } 
    public Expression<Func<string>> ValidationMessage { get; set; }
    public Dictionary<string, object?> PlaceholderValues { get; set; } = new Dictionary<string, object?>();

    private EmittableValidation(string errorCode, Expression validation, Expression<Func<string>> validationMessage)
    {
        ErrorCode = errorCode;
        Validation = validation;
        ValidationMessage = validationMessage;
    }

    public static EmittableValidation From(string errorCode, Expression validation, Expression<Func<string>> validationMessage) =>
        new EmittableValidation(errorCode, validation, validationMessage);

    public static EmittableValidation From<TDelegate>(string errorCode, Expression<TDelegate> validation, Expression<Func<string>> validationMessage) =>
        new EmittableValidation(errorCode, validation.ReplaceVariablesWithConstants(), validationMessage);
}