using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

[UsesVerify]
public class RuleBuilderSnapshotTests
{
    [Fact] public Task NotEmpty() => Builder().Of<int>().For("UserId").NotEmpty().VerifyRules();

    [Fact] public Task Length() => Builder().Of<string>().For("UserId").Length(1, 10).VerifyRules();

    [Fact] public Task ExactLength() => Builder().Of<string>().For("UserId").Length(10).VerifyRules();

    [Fact] public Task MinLength() => Builder().Of<string>().For("UserId").MinLength(10).VerifyRules();

    [Fact] public Task MaxLength() => Builder().Of<string>().For("UserId").MaxLength(10).VerifyRules();

    private TypelyBuilder Builder() => new TypelyBuilderFixture().Create();
}
