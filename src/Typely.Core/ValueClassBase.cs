using System;

namespace Typely.Core;

public abstract class ValueClassBase<TValue, TThis> : IValue<TValue, TThis> where TThis : ValueClassBase<TValue, TThis>
{
    public TValue Value { get; }

    protected ValueClassBase() { throw new Exception("Parameterless constructor not accessible."); }

    public ValueClassBase(TValue value)
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
#if NET5_0_OR_GREATER
        instance = isValid ? IValue<TValue, TThis>.From(value) : default;
#else
        instance = null;//TODO
#endif
        return isValid;
    }
}
