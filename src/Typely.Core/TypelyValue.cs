namespace Typely.Core;

public static class TypelyValue
{
    public static void ValidateAndThrow<TValue, TThis>(TValue value) where TThis : ITypelyValue<TValue, TThis>
    {
#if NET7_0_OR_GREATER
        var validationError = TThis.Validate(value);
        if (validationError != null)
        {
            throw new ArgumentException(validationError.ToString()); //Comment la désérialisation Json va fonctionner?
        }
#else
        throw new NotImplementedException();
#endif
    }
}