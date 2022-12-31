﻿using System.Diagnostics.CodeAnalysis;
using System.Resources;
using System.Text.Json.Serialization;
using Typely.Core;
using Typely.Core.Converters;

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
        var placeholderValues = new Dictionary<string, object?>
        {
            { "Name", "CustomName" }
        };

        //NotEmpty
        if (value != default)
        {
            return ValidationErrorFactory.Create(value, "NotEmpty", ErrorMessages.NotEmpty, nameof(ReferenceSample), placeholderValues);
        }

        //NotEmpty
        if (value != default) 
        {
            return ValidationErrorFactory.Create(value, "NotEmpty", ErrorMessages.NotEmpty, nameof(ReferenceSample), placeholderValues);
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