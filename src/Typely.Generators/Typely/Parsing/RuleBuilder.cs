using Typely.Core;

namespace Typely.Generators.Typely.Parsing;

internal class RuleBuilder<T> : TypelyBuilder<T>, IRuleBuilder<T>
{
    public RuleBuilder(EmittableType emittableType) : base(emittableType) { }

    public IRuleBuilder<T> WithErrorCode(string errorCode)
    {
        _emittableType.CurrentValidation!.ErrorCode = errorCode;
        return this;
    }

    public IRuleBuilder<T> WithMessage(string message)
    {
        _emittableType.CurrentValidation!.ValidationMessage = () => message;
        return this;
    }
}
