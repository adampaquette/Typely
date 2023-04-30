﻿//HintName: Election.Votes.g.cs
// <auto-generated>This file was generated by Typely.</auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Typely.Core;
using Typely.Core.Converters;
using Typely.Generators.Tests.Typely.Configurations;

#nullable enable

namespace Election
{
    [TypeConverter(typeof(TypelyTypeConverter<TimeOnly, Votes>))]
    [JsonConverter(typeof(TypelyJsonConverter<TimeOnly, Votes>))]
    public partial struct Votes : ITypelyValue<TimeOnly, Votes>, IEquatable<Votes>, IComparable<Votes>, IComparable
    {
        public TimeOnly Value { get; private set; }                    

        public Votes(TimeOnly value)
        {
            TypelyValue.ValidateAndThrow<TimeOnly, Votes>(value);
            Value = value;
        }

        public static ValidationError? Validate(TimeOnly value)
        {
            if (value == default)
            {
                return ValidationErrorFactory.Create(value, "ERR-001", "The value cannot be empty", "Presidency vote");
            }

            if (value.Equals(new TimeOnly(2021, 1, 1)))
            {
                return ValidationErrorFactory.Create(value, "NotEqual", ErrorMessages.NotEqual, "Presidency vote",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", new TimeOnly(2021, 1, 1) },
                    });
            }

            if (!(value == new TimeOnly(2022,1,1)))
            {
                return ValidationErrorFactory.Create(value, "Must", ErrorMessages.Must, "Presidency vote");
            }

            if (!(!value.Equals(10)))
            {
                return ValidationErrorFactory.Create(value, "Must", ErrorMessages.Must, "Presidency vote");
            }

            if (value <= new TimeOnly(2022,1,1))
            {
                return ValidationErrorFactory.Create(value, "GreaterThan", LocalizedMessages.CustomMessage, "Presidency vote",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", new TimeOnly(2022,1,1) },
                    });
            }

            if (value < new TimeOnly(2022,1,1))
            {
                return ValidationErrorFactory.Create(value, "GreaterThanOrEqualTo", A.CustomLocalization.Value, "Presidency vote",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", new TimeOnly(2022,1,1) },
                    });
            }

            if (value >= new TimeOnly(2022,1,1))
            {
                return ValidationErrorFactory.Create(value, "LessThan", ErrorMessages.LessThan, "Presidency vote",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", new TimeOnly(2022,1,1) },
                    });
            }

            if (value > new TimeOnly(2022,1,1))
            {
                return ValidationErrorFactory.Create(value, "LessThanOrEqualTo", ErrorMessages.LessThanOrEqualTo, "Presidency vote",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", new TimeOnly(2022,1,1) },
                    });
            }

            return null;
        }

        public static Votes From(TimeOnly value) => new(value);

        public static bool TryFrom(TimeOnly value, out Votes typelyType, out ValidationError? validationError)
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
        
        public static bool TryParse(string? value, IFormatProvider? provider, out Votes valueObject)
        {
            if(TimeOnly.TryParse(value, out var underlyingValue))
            {
                valueObject = From(underlyingValue);
                return true;
            }
                
            valueObject = default;
            return false;
        }

        public override string ToString() => Value.ToString();

        public static bool operator !=(Votes left, Votes right) => !(left == right);

        public static bool operator ==(Votes left, Votes right) => left.Equals(right);

        public override int GetHashCode() => Value.GetHashCode();

        public bool Equals(Votes other) => Value.Equals(other.Value);

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is Votes && Equals((Votes)obj);

        public int CompareTo(Votes other) => Value.CompareTo(other.Value);

        public int CompareTo(object? obj) => obj is not Votes ? 1 : CompareTo((Votes)obj!);

        public static explicit operator TimeOnly(Votes value) => value.Value;
    }
}