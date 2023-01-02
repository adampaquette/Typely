using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

[UsesVerify]
public class RuleBuilderSnapshotTests
{
    [Fact] public Task NotEmpty() => Builder().For<int>("UserId").NotEmpty().VerifyRules();

    [Fact] public Task Length() => Builder().For<string>("UserId").Length(1, 10).VerifyRules();

    [Fact] public Task ExactLength() => Builder().For<string>("UserId").Length(10).VerifyRules();

    [Fact] public Task MinLength() => Builder().For<string>("UserId").MinLength(10).VerifyRules();

    [Fact] public Task MaxLength() => Builder().For<string>("UserId").MaxLength(10).VerifyRules();

    private TypelyBuilder Builder() => new TypelyBuilderFixture().Create();
}
