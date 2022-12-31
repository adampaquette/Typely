using System.Diagnostics.CodeAnalysis;

namespace Typely.Core;

public interface ITypelyValue<out TValue>
{
    TValue Value { get; }
}

public interface ITypelyValue<TValue, TThis> : ITypelyValue<TValue> where TThis : ITypelyValue<TValue, TThis>
{
#if NET7_0_OR_GREATER
    static abstract ValidationError? Validate(TValue value);
    static abstract TThis From(TValue value);
    static abstract bool TryFrom(TValue value, [MaybeNullWhen(false)] out TThis instance, out ValidationError? validationError);
#endif
}