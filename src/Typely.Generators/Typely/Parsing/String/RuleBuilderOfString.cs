using System.Linq.Expressions;
using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing.String;

internal class RuleBuilderOfString : TypelyBuilderOfString, IRuleBuilderOfString
{
    private List<EmittableType> _emittableTypes;

    public RuleBuilderOfString(EmittableType emittableType, List<EmittableType> emittableTypes)
        : base(emittableType)
    {
        _emittableTypes = emittableTypes;
    }

    public IRuleBuilderOfString WithErrorCode(string errorCode)
    {
        _emittableType.CurrentRule!.ErrorCode = errorCode;
        return this;
    }

    public IRuleBuilderOfString WithMessage(string message)
    {
        _emittableType.CurrentRule!.Message = Expression.Lambda<Func<string>>(Expression.Constant(message));
        return this;
    }

    public IRuleBuilderOfString WithMessage(Expression<Func<string>> expression)
    {
        _emittableType.CurrentRule!.Message = expression;
        return this;
    }

    public IReadOnlyList<EmittableType> GetEmittableTypes() => _emittableTypes.AsReadOnly();
}
