using System.Collections.Immutable;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

public class EmittableRuleTests
{
    [Fact]
    public void TwoRulesWithTheSameProperties_ShouldBe_Equal()
    {
        var rule1 = new EmittableRule("string", "Name", "Name",
            new Dictionary<string, object?> { { "MaxLength", "10" } }.ToImmutableDictionary());

        var rule2 = new EmittableRule("string", "Name", "Name",
            new Dictionary<string, object?> { { "MaxLength", "10" } }.ToImmutableDictionary());

        Assert.True(rule1.Equals(rule2));
        Assert.Equal(rule1.GetHashCode(), rule2.GetHashCode());
    }

    [Fact]
    public void TwoRulesWithOneDiffObject_ShouldNotBe_Equal()
    {
        var rule1 = new EmittableRule("string", "Name", "Name",
            new Dictionary<string, object?> { { "MaxLength", "10" } }.ToImmutableDictionary());

        // ReSharper disable once SuspiciousTypeConversion.Global
        Assert.False(rule1.Equals("value"));
    }
}