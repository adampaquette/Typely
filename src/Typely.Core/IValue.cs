using System;
using System.Diagnostics.CodeAnalysis;

namespace Typely.Core;

public interface IValue<out TValue>
{
    TValue Value { get; }
}

public interface IValue<TValue, TThis> : IValue<TValue> where TThis : IValue<TValue, TThis>
{
#if NET7_0_OR_GREATER
    static abstract ValidationError? Validate(TValue value);
    static abstract TThis From(TValue value);
    static abstract bool TryFrom(TValue value, [MaybeNullWhen(false)] out TThis instance, out ValidationError? validationError);
#endif
}