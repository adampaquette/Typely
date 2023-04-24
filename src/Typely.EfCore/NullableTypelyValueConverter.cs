using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Typely.Core;

namespace Typely.EfCore;

/// <summary>
/// Defines conversions from an <see cref="ITypelyValue{TValue,TTypelyValue}" /> to the underlying value's type, in the store.
/// </summary>
/// <remarks>
/// See <see href="https://aka.ms/efcore-docs-value-converters">EF Core value converters</see> for more information and examples.
/// </remarks>
public class NullableTypelyValueConverter<TValue, TTypelyValue> : ValueConverter<TTypelyValue?, TValue?>
    where TValue : struct
    where TTypelyValue : struct, ITypelyValue<TValue, TTypelyValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TypelyValueConverter{TValue,TTypelyValue}" /> class.
    /// </summary>
    public NullableTypelyValueConverter()
        : base(
            v => v.HasValue ? v.Value.Value : null,
            v => v.HasValue ? TypelyValue.From<TValue, TTypelyValue>(v.Value) : null)
    {
    }
}