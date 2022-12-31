using System.Linq.Expressions;

namespace Typely.Generators.Typely.Parsing;

internal class EmittableValidation
{
    public Expression Validation { get; }
    public string ErrorCode { get; set; } 
    public Expression<Func<string>> ValidationMessage { get; set; }

    public EmittableValidation(string errorCode, Expression validation, Expression<Func<string>> validationMessage)
    {
        ErrorCode = errorCode;
        Validation = validation;
        ValidationMessage = validationMessage;
    }
}