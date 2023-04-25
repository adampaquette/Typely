using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Typely.Core;
using Typely.Core.Builders;
using Typely.Core.Converters;

namespace Typely.EfCore.Tests;

public class TypelyConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfString().For("MyString");
        builder.OfInt().For("ValueTypeWithUnderlyingValueType");
        builder.OfString().For("ValueTypeWithUnderlyingReferenceType");

        //builder.OfInt().For("ReferenceTypeWithUnderlyingValueType").AsClass();
        //builder.OfString().For("ReferenceTypeWithUnderlyingReferenceType").AsClass();
    }
}

public class TypelyValueConverterTests
{
    public class ValueTypeWithUnderlyingValueTypeTests
    {
        public class NonNullable
        {
            private const int expectedValue = 1;
            private readonly TypelyValueConverter<int, ValueTypeWithUnderlyingValueType> converter = new();            
            private readonly ValueTypeWithUnderlyingValueType expected = ValueTypeWithUnderlyingValueType.From(expectedValue);

            [Fact]
            public void ConvertFromProvider() => Assert.Equal(expected, converter.ConvertFromProvider(expectedValue));

            [Fact]
            public void ConvertToProvider() => Assert.Equal(expectedValue, converter.ConvertToProvider(expected));
        }

        // public class Nullable
        // {
        //     private const int expectedValue = 1;
        //     private readonly NullableTypelyValueConverter<int, ValueTypeWithUnderlyingValueType?> converter = new();
        //     private readonly ValueTypeWithUnderlyingValueType expected = ValueTypeWithUnderlyingValueType.From(expectedValue);
        //
        //     [Fact]
        //     public void ConvertFromProvider() => Assert.Equal(expected, converter.ConvertFromProvider(expectedValue));
        //
        //     [Fact]
        //     public void ConvertToProvider() => Assert.Equal(expectedValue, converter.ConvertToProvider(expected));
        // }
    }
}

// public partial class ReferenceTypeWithUnderlyingReferenceType : ITypelyValue<string, ReferenceTypeWithUnderlyingReferenceType>, IEquatable<ReferenceTypeWithUnderlyingReferenceType>, IComparable<ReferenceTypeWithUnderlyingReferenceType>, IComparable
// {
//     public string Value { get; private set; }
//
//     public ReferenceTypeWithUnderlyingReferenceType(string value)
//     {
//         TypelyValue.ValidateAndThrow<string, ReferenceTypeWithUnderlyingReferenceType>(value);
//         Value = value;
//     }
//
//     private ReferenceTypeWithUnderlyingReferenceType(string value, bool unsafeCtor)
//     {
//         Value = value;
//     }
//
//     public static ValidationError? Validate(string value)
//     {
//         if (value == null) throw new ArgumentNullException(nameof(ReferenceTypeWithUnderlyingReferenceType));
//
//         return null;
//     }
//
//     public static ReferenceTypeWithUnderlyingReferenceType From(string value) => new(value);
//
//     public static bool TryFrom(string value, [MaybeNullWhen(false)] out ReferenceTypeWithUnderlyingReferenceType? typelyType, out ValidationError? validationError)
//     {
//         validationError = Validate(value);
//         var isValid = validationError == null;
//
//         if (isValid)
//         {
//             typelyType = new ReferenceTypeWithUnderlyingReferenceType(value, true);
//             typelyType.Value = value;
//         }
//         else
//         {
//             typelyType = default;
//         }
//         return isValid;
//     }
//
//     public override string ToString() => Value.ToString();
//
//     public static bool operator !=(ReferenceTypeWithUnderlyingReferenceType? left, ReferenceTypeWithUnderlyingReferenceType? right) => !(left == right);
//
//     public static bool operator ==(ReferenceTypeWithUnderlyingReferenceType? left, ReferenceTypeWithUnderlyingReferenceType? right) => left != null && left.Equals(right);
//
//     public override int GetHashCode() => Value.GetHashCode();
//
//     public bool Equals(ReferenceTypeWithUnderlyingReferenceType? other) => other == null ? false : Value.Equals(other.Value);
//
//     public override bool Equals([NotNullWhen(true)] object? obj) => obj is ReferenceTypeWithUnderlyingReferenceType && Equals((ReferenceTypeWithUnderlyingReferenceType)obj);
//
//     public int CompareTo(ReferenceTypeWithUnderlyingReferenceType? other) => other == null ? 1 : Value.CompareTo(other.Value);
//
//     public int CompareTo(object? obj) => obj is not ReferenceTypeWithUnderlyingReferenceType ? 1 : CompareTo((ReferenceTypeWithUnderlyingReferenceType)obj!);
//
//     public static explicit operator string(ReferenceTypeWithUnderlyingReferenceType value) => value.Value;
// }