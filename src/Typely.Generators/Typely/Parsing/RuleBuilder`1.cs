using System.Linq.Expressions;
using static Typely.Generators.TypelyGenerator;
using Typely.Core;

namespace Typely.Generators.Typely.Parsing;

internal class RuleBuilder<T> : TypelyBuilder<T>, IRuleBuilder<T>
{
    public RuleBuilder(EmittableType emittableType) : base(emittableType) { }

    public IRuleBuilder<T> When(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<T> WithMessage(string message)
    {
        throw new NotImplementedException();
    }
}
