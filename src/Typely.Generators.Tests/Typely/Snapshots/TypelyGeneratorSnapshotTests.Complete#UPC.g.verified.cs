﻿//HintName: UPC.g.cs
// <auto-generated>This file was generated by Typely.</auto-generated>
using Typely.Core;
using Typely.Core.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

#nullable enable

namespace Typely.Generators.Tests.Typely.Configurations
{
    [JsonConverter(typeof(TypelyJsonConverter<Int32, UPC>))]
    public partial struct UPC : ITypelyValue<Int32, UPC>, IEquatable<UPC>, IComparable<UPC>, IComparable
    {
        public Int32 Value { get; private set; }

        public UPC() => throw new Exception("Parameterless constructor is not allowed.");

        public UPC(Int32 value)
        {
            TypelyValue.ValidateAndThrow<Int32, UPC>(value);
            Value = value;
        }

        public static ValidationError? Validate(Int32 value)
        {
            if (EqualityComparer<int>.Default.Equals(value, 0))
            {
                return ValidationErrorFactory.Create(value, "NotEmpty", ErrorMessages.NotEqual, "UPC");
            }

            return null;
        }

        public static UPC From(Int32 value) => new(value);

        public static bool TryFrom(Int32 value, [MaybeNullWhen(false)] out UPC typelyType, out ValidationError? validationError)
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

        public static bool operator !=(UPC left, UPC right) => !(left == right);

        public static bool operator ==(UPC left, UPC right) => left.Equals(right);

        public override int GetHashCode() => Value.GetHashCode();

        public bool Equals(UPC other) => Value.Equals(other.Value);

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is UPC && Equals((UPC)obj);

        public int CompareTo(UPC other) => Value.CompareTo(other.Value);

        public int CompareTo(object? obj) => obj is not UPC ? 1 : CompareTo((UPC)obj!);

        public static explicit operator Int32(UPC value) => value.Value;
    }
}