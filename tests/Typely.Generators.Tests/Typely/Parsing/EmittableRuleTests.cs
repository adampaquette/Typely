using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

public class EmittableRuleTests
{
    [Fact]
    public void TwoRulesWithTheSameProperties_ShouldBe_Equal()
    {
        var rule1 = new EmittableRule("string", "Name", "Name",
            new Dictionary<string, string> { { "MaxLength", "10" } });

        var rule2 = new EmittableRule("string", "Name", "Name",
            new Dictionary<string, string> { { "MaxLength", "10" } });

        Assert.Equal(rule1, rule2);
    }
}