using System.Resources;
using Typely.Core;

namespace Typely.Tests;

public class ReferenceSample : IValue<int, ReferenceSample>
{
    public int Value { get; }

    public ReferenceSample(int value)
    {
        Value = value;
    }

    public static ReferenceSample From(int value)
    {
        var result = Validate(value);
        if(result != null)
        {
            throw new ArgumentException(result.ToString()); //Comment la désérialisation Json va fonctionner?
        }

        return new ReferenceSample(value);
    }

    public static ValidationError? Validate(int value)
    {
        if (value == default)
        {
            return new ValidationError("ErrorCode", "ErrorMessage", "ErrorMessageWithPlaceholders", value, "ReferenceSample");
        }
        return null;
    }
}

public class NotEmptyValidationEmitter
{
    public string Emit<T>(T type, ResourceManager resourceManager)
    {
        return ErrorMessages.NotEmpty;
    }
}