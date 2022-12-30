using System;

namespace Typely.Core;

public interface IValue<out TValue>
{
    TValue Value { get; }
}

public interface IValue<TValue, TThis> : IValue<TValue> where TThis : IValue<TValue, TThis>
{
#if NET5_0_OR_GREATER
    //static TThis From(int value)
    //{
    //    var result = Validate(value);
    //    if (result != null)
    //    {
    //        throw new ArgumentException(result.ToString()); //Comment la désérialisation Json va fonctionner?
    //    }

    //    return new TThis(value);
    //}

    static ValidationError? Validate(TValue value) => null;

    static bool TryFrom(TValue value, out TThis? instance, out ValidationError? validationError)
    {
        validationError = Validate(value);
        var isValid = validationError != null;
        instance = isValid ? IValue<TValue, TThis>.From(value) : default;
        return isValid;
    }

    static TThis From(TValue value) => throw new NotImplementedException();
#endif
}