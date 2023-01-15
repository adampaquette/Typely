using System.Linq.Expressions;
using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing.Int;

/// <summary>
/// Rule builder of string.
/// </summary>
internal class RuleBuilderOfInt : TypelyBuilderOfInt, IRuleBuilderOfInt
{
    public RuleBuilderOfInt(EmittableType emittableType, List<EmittableType> emittableTypes)
        : base(emittableType, emittableTypes)
    {
    }

    /// <inheritdoc/>
    public IRuleBuilderOfInt WithErrorCode(string errorCode)
    {
        EmittableType.SetCurrentErrorCode(errorCode);
        return this;
    }

    /// <inheritdoc/>
    public IRuleBuilderOfInt WithMessage(string message)
    {
        EmittableType.SetCurrentMessage(message);
        return this;
    }

    /// <inheritdoc/>
    public IRuleBuilderOfInt WithMessage(Expression<Func<string>> expression)
    {
        EmittableType.SetCurrentMessage(expression);
        return this;
    }

    public IReadOnlyList<EmittableType> GetEmittableTypes() => EmittableTypes.AsReadOnly();
}