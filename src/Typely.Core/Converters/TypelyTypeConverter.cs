using System.ComponentModel;
using System.Globalization;

namespace Typely.Core.Converters;

/// <summary>
/// Provides a way of converting <see cref="ITypelyValue{TValue,TTypelyValue}"/> types to their underlying value type and vice versa.
/// </summary>
/// <typeparam name="TValue">The underlying type of the value object.</typeparam>
/// <typeparam name="TTypelyValue">Value object's type.</typeparam>
public class TypelyTypeConverter<TValue, TTypelyValue> : TypeConverter
    where TTypelyValue : ITypelyValue<TValue, TTypelyValue>
{
    private readonly TypeConverter _underlyingValueConverter = TypeDescriptor.GetConverter(typeof(TValue));

    /// <inheritdoc/>
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) =>
        sourceType == typeof(TValue) || _underlyingValueConverter.CanConvertFrom(context, sourceType);

    /// <inheritdoc/>
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) =>
        destinationType == typeof(TValue) || _underlyingValueConverter.CanConvertTo(context, destinationType);

    /// <inheritdoc/>
    public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is not TValue underlyingValue)
        {
            underlyingValue = (TValue)_underlyingValueConverter.ConvertFrom(context, culture, value)!;
        }

        return TypelyValue.From<TValue, TTypelyValue>(underlyingValue);
    }

    /// <inheritdoc/>
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value,
        Type destinationType) =>
        value is not ITypelyValue<TValue, TTypelyValue> typelyValue ? default(object?) : typelyValue.Value;
}