using Typely.Core;

namespace Typely.Generators.Typely.Parsing;

internal class RuleBuilder<T> : TypelyBuilder<T>, IRuleBuilder<T>
{
    public RuleBuilder(EmittableType emittableType) : base(emittableType) { }

    public IRuleBuilder<T> WithMessage(string message)
    {
        throw new NotImplementedException();
    }
}
