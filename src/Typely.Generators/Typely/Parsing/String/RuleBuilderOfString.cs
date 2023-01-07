using System.Linq.Expressions;
using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing.String;

internal class RuleBuilderOfString : TypelyBuilderOfString, IRuleBuilderOfString
{
    public RuleBuilderOfString(EmittableType emittableType, List<EmittableType> emittableTypes)
        : base(emittableType, emittableTypes)
    {        
    }

    public IRuleBuilderOfString WithErrorCode(string errorCode)
    {
        EmittableType.CurrentRule!.ErrorCode = errorCode;
        return this;
    }

    public IRuleBuilderOfString WithMessage(string message)
    {
        EmittableType.CurrentRule!.Message = Expression.Lambda<Func<string>>(Expression.Constant(message));
        return this;
    }

    public IRuleBuilderOfString WithMessage(Expression<Func<string>> expression)
    {
        EmittableType.CurrentRule!.Message = expression;
        return this;
    }

    public IReadOnlyList<EmittableType> GetEmittableTypes() => EmittableTypes.AsReadOnly();
}
