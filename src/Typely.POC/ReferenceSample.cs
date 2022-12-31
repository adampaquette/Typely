using System.Resources;
using Typely.Core;
using Typely.Core.Converters;
using Typely.POC;

namespace Typely.Tests;

[System.Text.Json.Serialization.JsonConverter(typeof(TypelyJsonConverter<int, ReferenceSample>))]
public partial struct ReferenceSample : ITypelyValue<int, ReferenceSample>
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
        var isValid = validationError == null;
        instance = default;
        if (isValid)
        {
            // Bypass constructor to avoid validating again
            instance.Value = value;
        }
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