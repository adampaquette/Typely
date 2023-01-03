using System.Linq.Expressions;
using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing;

internal class RuleBuilder<TValue> : TypelyBuilder<TValue>,
    ITypelyBuilder<TValue, RuleBuilder<TValue>, TypelyBuilder<TValue>>
{
    private List<EmittableType> _emittableTypes;

    public RuleBuilder(EmittableType emittableType, List<EmittableType> emittableTypes) : base(emittableType)
    {
        _emittableTypes = emittableTypes;
    }

    public RuleBuilder<TValue> WithErrorCode(string errorCode)
    {
        _emittableType.CurrentValidation!.ErrorCode = errorCode;
        return this;
    }

    public RuleBuilder<TValue> WithMessage(string message)
    {
        _emittableType.CurrentValidation!.ValidationMessage = Expression.Lambda<Func<string>>(Expression.Constant(message));
        return this;
    }

    public RuleBuilder<TValue> WithMessage(Expression<Func<string>> expression)
    {
        _emittableType.CurrentValidation!.ValidationMessage = expression;
        return this;
    }

    public IReadOnlyList<EmittableType> GetEmittableTypes() => _emittableTypes.AsReadOnly();
}
