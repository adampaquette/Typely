﻿//HintName: Typely.Generators.Tests.Typely.Configurations.B.Name.g.cs
// <auto-generated>This file was generated by Typely.</auto-generated>
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Typely.Core;
using Typely.Core.Converters;

#nullable enable

namespace Typely.Generators.Tests.Typely.Configurations.B
{
    [JsonConverter(typeof(TypelyJsonConverter<string, Name>))]
    public partial struct Name : ITypelyValue<string, Name>, IEquatable<Name>, IComparable<Name>, IComparable
    {
        public string Value { get; private set; }

        public Name() => throw new Exception("Parameterless constructor is not allowed.");

        public Name(string value)
        {
            TypelyValue.ValidateAndThrow<string, Name>(value);
            Value = value;
        }

        public static ValidationError? Validate(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(Name));

            return null;
        }

        public static Name From(string value) => new(value);

        public static bool TryFrom(string value, [MaybeNullWhen(false)] out Name typelyType, out ValidationError? validationError)
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

        public static bool operator !=(Name left, Name right) => !(left == right);

        public static bool operator ==(Name left, Name right) => left.Equals(right);

        public override int GetHashCode() => Value.GetHashCode();

        public bool Equals(Name other) => Value.Equals(other.Value);

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is Name && Equals((Name)obj);

        public int CompareTo(Name other) => Value.CompareTo(other.Value);

        public int CompareTo(object? obj) => obj is not Name ? 1 : CompareTo((Name)obj!);

        public static explicit operator string(Name value) => value.Value;
    }
}