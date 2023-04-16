using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Typely.Core;

namespace Typely.EfCore;

/// <summary>
/// Defines conversions from an <see cref="ITypelyValue{TValue,TTypelyValue}" /> to the underlying value's type, in the store.
/// </summary>
/// <remarks>
/// See <see href="https://aka.ms/efcore-docs-value-converters">EF Core value converters</see> for more information and examples.
/// </remarks>
public class TypelyValueConverter<TValue, TTypelyValue> : ValueConverter<TTypelyValue, TValue>
    where TTypelyValue : ITypelyValue<TValue, TTypelyValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TypelyValueConverter{TValue,TTypelyValue}" /> class.
    /// </summary>
    public TypelyValueConverter()
        : base(
            v => v.Value,
            v => TypelyValue.From<TValue, TTypelyValue>(v))
    {
    }
}