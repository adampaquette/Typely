using System.Diagnostics.CodeAnalysis;
using System.Resources;
using Typely.Core;
using Typely.POC;

namespace Typely.Tests;

public static class TypelyValue
{
    public static void ValidateAndThrow<TValue, TThis>(TValue value) where TThis : IValue<TValue, TThis>
    {
        var validationError = TThis.Validate(value);
        if (validationError != null)
        {
            throw new ArgumentException(validationError.ToString()); //Comment la désérialisation Json va fonctionner?
        }
    }

    public static bool TryFrom<TValue, TThis>(TValue value, [MaybeNullWhen(false)] out TThis instance, out ValidationError? validationError)
        where TThis : IValue<TValue, TThis>
    {
        validationError = TThis.Validate(value);
        var isValid = validationError != null;
        instance = isValid ? TThis.From(value) : default;
        return isValid;
    }
}

public partial struct ReferenceSample : IValue<int, ReferenceSample>
{
    public int Value { get; private set; }

    public ReferenceSample() => throw new Exception("Parameterless constructor not accessible.");

    public ReferenceSample(int value)
    {
        TypelyValue.ValidateAndThrow<int, ReferenceSample>(value);
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

    public static bool TryFrom(int value, out ReferenceSample instance, out ValidationError? validationError)
    {
        validationError = Validate(value);
        var isValid = validationError != null;
        instance = default;
        if (isValid)
        {
            // Bypass constructor to avoid validating again
            instance.Value = value;
        }
        return isValid;
    }

    public static bool TryFrom2(int value, out ReferenceSample instance, out ValidationError? validationError) =>
        TypelyValue.TryFrom(value, out instance, out validationError);
}


public class NotEmptyValidationEmitter
{
    public string Emit<T>(T type, ResourceManager resourceManager)
    {
        return ErrorMessages.NotEmpty;
    }
}