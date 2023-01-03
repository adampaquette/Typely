using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Parsing.String;

[UsesVerify]
public class TypelyBuilderOfStringSnapshotTests
{
    [Fact] public Task NotEmpty() => Builder().NotEmpty().VerifyRules();

    [Fact] public Task Length() => Builder().Length(1, 10).VerifyRules();

    [Fact] public Task ExactLength() => Builder().Length(10).VerifyRules();

    [Fact] public Task MinLength() => Builder().MinLength(10).VerifyRules();

    [Fact] public Task MaxLength() => Builder().MaxLength(10).VerifyRules();

    [Fact] public Task LessThan() => Builder().LessThan("20").VerifyRules();

    [Fact] public Task LessThanOrEqualTo() => Builder().LessThanOrEqual("20").VerifyRules();

    [Fact] public Task GreaterThan() => Builder().GreaterThan("20").VerifyRules();

    [Fact] public Task GreaterThanOrEqualTo() => Builder().GreaterThanOrEqual("20").VerifyRules();

    private ITypelyBuilderOfString Builder() => new TypelyBuilderFixture().Create().String().For("Name");
}
