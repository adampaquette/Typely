using System.Linq.Expressions;
using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing.Builders;

internal class RuleBuilderBase<TValue, TThis> : TypelyBuilderBase<TValue, TThis, TThis>
    where TThis : RuleBuilderBase<TValue, TThis>
{
    private List<EmittableType> _emittableTypes;

    public RuleBuilderBase(EmittableType emittableType, List<EmittableType> emittableTypes) 
        : base(emittableType)
    {
        _emittableTypes = emittableTypes;
    }

    public RuleBuilderBase<TValue, TThis> WithErrorCode(string errorCode)
    {
        _emittableType.CurrentValidation!.ErrorCode = errorCode;
        return this;
    }

    public RuleBuilderBase<TValue, TThis> WithMessage(string message)
    {
        _emittableType.CurrentValidation!.ValidationMessage = Expression.Lambda<Func<string>>(Expression.Constant(message));
        return this;
    }

    public RuleBuilderBase<TValue, TThis> WithMessage(Expression<Func<string>> expression)
    {
        _emittableType.CurrentValidation!.ValidationMessage = expression;
        return this;
    }

    public IReadOnlyList<EmittableType> GetEmittableTypes() => _emittableTypes.AsReadOnly();
}
