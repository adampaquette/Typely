using Typely.Core.Builders;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

[UsesVerify]
public class RuleBuilderSnapshotTests
{
    [Fact] public Task NotEmptyString() => StringBuilder().NotEmpty().VerifyRules();

    [Fact] public Task NotEmptyInt() => IntBuilder().NotEmpty().VerifyRules();

    [Fact] public Task Length() => StringBuilder().Length(1, 10).VerifyRules();

    [Fact] public Task ExactLength() => StringBuilder().Length(10).VerifyRules();

    [Fact] public Task MinLength() => StringBuilder().MinLength(10).VerifyRules();

    [Fact] public Task MaxLength() => StringBuilder().MaxLength(10).VerifyRules();

    [Fact] public Task LessThan() => IntBuilder().LessThan(20).VerifyRules();

    [Fact] public Task LessThanOrEqualTo() => IntBuilder().LessThanOrEqual(20).VerifyRules();

    [Fact] public Task GreaterThan() => IntBuilder().GreaterThan(20).VerifyRules();

    [Fact] public Task GreaterThanOrEqualTo() => IntBuilder().GreaterThanOrEqual(20).VerifyRules();

    private TypelyBuilder Builder() => new TypelyBuilderFixture().Create();

    private ITypelyBuilder<string> StringBuilder() => Builder().Of<string>().For("Name");

    private ITypelyBuilder<int> IntBuilder() => Builder().Of<int>().For("Age");
}
