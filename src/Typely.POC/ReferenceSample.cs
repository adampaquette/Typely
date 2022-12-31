using System.Resources;
using Typely.Core;
using Typely.POC;

namespace Typely.Tests;

public struct Value
{ 

}

public readonly partial struct ReferenceSample : IValue<int, ReferenceSample>
{
    public int Value { get; }

    public ReferenceSample() => throw new Exception("Parameterless constructor not accessible.");

    public ReferenceSample(int value)
    {
        ValidateAndThrow(value);
        Value = value;
    }
    
    public static void ValidateAndThrow(int value)
    {
        var validationError = Validate(value);
        if (validationError != null)
        {
            throw new ArgumentException(validationError.ToString()); //Comment la désérialisation Json va fonctionner?
        }
    }

    public static ValidationError? Validate(int value)
    {
        if (value < 0)
        {
            return new ValidationError("ERR001", "Value can't be negative", "Value can't be negative", value, "ValidationError");
        }
        return null;
    }

    public static ReferenceSample From(int value) => new(value);

    public static bool TryFrom(int value, out ReferenceSample instance, out ValidationError? validationError)
    {
        validationError = Validate(value);
        var isValid = validationError != null;
        instance = isValid ? new(value) : default;
        return isValid;
    }


}


public class NotEmptyValidationEmitter
{
    public string Emit<T>(T type, ResourceManager resourceManager)
    {
        return ErrorMessages.NotEmpty;
    }
}