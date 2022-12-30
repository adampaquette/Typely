﻿using System;

namespace Typely.Core;

public abstract struct ValueBase<TValue, TThis> : IValue<TValue, TThis> where TThis : ValueBase<TValue, TThis>
{
    public TValue Value { get; }

    protected ValueBase() { throw new Exception("Parameterless constructor not accessible."); }

    public ValueBase(TValue value)
    {
        ValidateAndThrow(value);
        Value = value;
    }

    public static ValidationError? Validate(TValue value) => null;

    public static void ValidateAndThrow(TValue value)
    {
        var validationError = Validate(value);
        if (validationError != null)
        {
            throw new ArgumentException(validationError.ToString()); //Comment la désérialisation Json va fonctionner?
        }
    }

    public static bool TryFrom(TValue value, out TThis? instance, out ValidationError? validationError)
    {
        validationError = Validate(value);
        var isValid = validationError != null;
        instance = isValid ? IValue<TValue, TThis>.From(value) : default;
        return isValid;
    }
}
