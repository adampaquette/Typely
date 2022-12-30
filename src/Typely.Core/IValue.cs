using System;

namespace Typely.Core;

public interface IValue<out TValue>
{
    TValue Value { get; }
}

public interface IValue<TValue, TThis> : IValue<TValue> where TThis : IValue<TValue, TThis>
{
#if NET5_0_OR_GREATER
    static ValidationError? Validate(TValue value) => null;

    static void ValidateAndThrow(TValue value)
    {
        var validationError = Validate(value);
        if (validationError != null)
        {
            throw new ArgumentException(validationError.ToString()); //Comment la désérialisation Json va fonctionner?
        }
    }

    static TThis From(TValue value) => throw new NotImplementedException();
    static bool TryFrom(TValue value, out TThis? instance, out ValidationError? validationError) => throw new NotImplementedException();
#endif
}