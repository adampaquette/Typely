using System.Diagnostics.CodeAnalysis;
using System.Resources;
using System.Text.Json.Serialization;
using Typely.Core;
using Typely.Core.Converters;
using Typely.POC;

namespace Typely.Tests;

[JsonConverter(typeof(TypelyJsonConverter<int, ReferenceSample>))]
public partial struct ReferenceSample : ITypelyValue<int, ReferenceSample>
{
    public int Value { get; private set; }

    public ReferenceSample() => throw new Exception("Parameterless constructor is not allowed.");

    public ReferenceSample(int value)
    {
        TypelyValue.ValidateAndThrow<int, ReferenceSample>(value);
        Value = value;
    }

    public static ValidationError? Validate(int value)
    {
        if (value < 0)
        {
            var placeholderValues = new Dictionary<string, object>
                {
                    { "Name", "ReferenceSample" },
                };

            object? attemptedValue = value;

            if(TypelyOptions.Instance.IsSensitiveDataLoggingEnabled)
            {
                placeholderValues.Add("Value", value);
                attemptedValue = null;
            }

            return new ValidationError(
                errorCode: "NotEmpty",
                errorMessage: "'{Name}' must not be empty",
                attemptedValue: attemptedValue,
                source: "ReferenceSample",
                errorMessageWithPlaceholders: "'{ReferenceSample}' must not be empty",
                placeholderValues: placeholderValues);
        }
        return null;
    }

    public static ReferenceSample From(int value) => new(value);

    public static bool TryFrom(int value, [MaybeNullWhen(false)] out ReferenceSample typelyType, out ValidationError? validationError)
    {
        validationError = Validate(value);
        var isValid = validationError == null;
        typelyType = default;
        if (isValid)
        {
            typelyType.Value = value;
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