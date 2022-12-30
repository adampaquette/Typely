using System;

namespace Typely.Core;

public interface IValidatable<TValue, TThis> where TThis : IValue<TValue, TThis>
{
#if NET5_0_OR_GREATER
    static ValidationError? Validate(TValue value) => throw new NotImplementedException();
    static bool TryFrom(TValue value, out TThis? instance, out ValidationError? validationError) => throw new NotImplementedException();
#endif
}