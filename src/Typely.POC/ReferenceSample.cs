using System.Resources;
using Typely.Core;
using Typely.POC;

namespace Typely.Tests;

public class ReferenceSample2 : ValueClassBase<int, ReferenceSample2>
{
    private ReferenceSample2() { }
    public ReferenceSample2(int value) : base(value) { }    
    public static ReferenceSample2 From(int value) => new(value);
}

public readonly struct ReferenceSample : IValue<int, ReferenceSample>
{
    public int Value { get; }

    public ReferenceSample() => throw new Exception("Parameterless constructor not accessible.");

    public ReferenceSample(int value)
    {
        IValue<int, ReferenceSample>.ValidateAndThrow(value);
        Value = value;
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

    public static bool TryFrom(int value, out ReferenceSample? instance, out ValidationError? validationError)
    {
        validationError = Validate(value);
        var isValid = validationError != null;
        instance = isValid ? IValue<int, ReferenceSample>.From(value) : default;
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