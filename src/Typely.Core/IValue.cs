using System;

namespace Typely.Core;

public interface IValue<out TValue>
{
    TValue Value { get; }
}

public interface IValue<TValue, TThis> : IValue<TValue> where TThis : IValue<TValue, TThis>
{
#if NET5_0_OR_GREATER
    static TThis From(TValue value) => throw new NotImplementedException();
#endif
}