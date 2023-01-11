using System.Linq.Expressions;
using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing.String;

/// <summary>
/// Rule builder of string.
/// </summary>
internal class RuleBuilderOfString : TypelyBuilderOfString, IRuleBuilderOfString
{
    public RuleBuilderOfString(EmittableType emittableType, List<EmittableType> emittableTypes)
        : base(emittableType, emittableTypes)
    {
    }

    public IRuleBuilderOfString WithErrorCode(string errorCode)
    {
        EmittableType.SetCurrentErrorCode(errorCode);
        return this;
    }

    public IRuleBuilderOfString WithMessage(string message)
    {
        EmittableType.SetCurrentMessage(message);
        return this;
    }

    public IRuleBuilderOfString WithMessage(Expression<Func<string>> expression)
    {
        EmittableType.SetCurrentMessage(expression);
        return this;
    }

    public IReadOnlyList<EmittableType> GetEmittableTypes() => EmittableTypes.AsReadOnly();
}
