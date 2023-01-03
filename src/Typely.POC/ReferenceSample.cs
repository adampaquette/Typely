using System.Diagnostics.CodeAnalysis;
using System.Resources;
using System.Text.Json.Serialization;
using Typely.Core;
using Typely.Core.Converters;

namespace Typely.POC;

#nullable enable

[JsonConverter(typeof(TypelyJsonConverter<int, ReferenceSample>))]
public partial struct ReferenceSample : ITypelyValue<int, ReferenceSample>, IEquatable<ReferenceSample>, IComparable<ReferenceSample>, IComparable
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
        //NotEmpty
        if (value == default)
        {
            return ValidationErrorFactory.Create(value, "NotEmpty", ErrorMessages.NotEmpty, nameof(ReferenceSample),
                new Dictionary<string, object?>
                {
                    { "Name", "My sample object" }
                });
        }

        //NotEmpty
        if (value == default)
        {
            return ValidationErrorFactory.Create(value, "NotEmpty", ErrorMessages.NotEmpty, nameof(ReferenceSample));
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

    public override string ToString() => Value.ToString();

    public static bool operator !=(ReferenceSample left, ReferenceSample right) => !(left == right);

    public static bool operator ==(ReferenceSample left, ReferenceSample right) => left.Equals(right);

    public override int GetHashCode() => Value.GetHashCode();

    public bool Equals(ReferenceSample other) => other.Value.Equals(Value);

    public override bool Equals([NotNullWhen(true)] object? obj) => obj is ReferenceSample && Equals((ReferenceSample)obj);

    public int CompareTo(ReferenceSample other) => other.Value.CompareTo(Value);

    public int CompareTo(object? obj) => obj is not ReferenceSample ? 1 : CompareTo((ReferenceSample)obj!);

    public static explicit operator int(ReferenceSample value) => value.Value;
}

public record TestR(int Value);

public class NotEmptyValidationEmitter
{
    public string Emit<T>(T type, ResourceManager resourceManager)
    {
        return ErrorMessages.NotEmpty;
    }
}