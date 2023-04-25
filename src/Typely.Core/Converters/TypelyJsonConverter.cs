#if NET7_0_OR_GREATER

using System.Text.Json.Serialization;
using System.Text.Json;

namespace Typely.Core.Converters;

/// <summary>
/// Json converter for typely types.
/// </summary>
/// <typeparam name="TValue">The wrapped type.</typeparam>
/// <typeparam name="TTypelyValue">The value object type.</typeparam>
public class TypelyJsonConverter<TValue, TTypelyValue> : JsonConverter<TTypelyValue> where TTypelyValue : ITypelyValue<TValue, TTypelyValue>
{
    /// <inheritdoc/>
    public override TTypelyValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = JsonSerializer.Deserialize<TValue>(ref reader, options);
        if(value is null)
        {
            return default;
        }

        return TTypelyValue.TryFrom(value, out var typelyType, out var validationError) 
            ? typelyType 
            : throw new ValidationException(validationError!);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TTypelyValue typelyType, JsonSerializerOptions options) =>
        JsonSerializer.Serialize(writer, typelyType.Value, options);
}
#endif