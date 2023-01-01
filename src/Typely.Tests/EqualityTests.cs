using Newtonsoft.Json.Linq;

namespace Typely.Tests;

public class EqualityTests
{
    [Fact]
    public void Operator_Should_Equal()
    {
        var value = 1;

        var left = EqualityTest.From(value);
        var right = EqualityTest.From(value);

        Assert.True(left.Equals(left));
        Assert.True(left.Equals(right));
        Assert.True(right.Equals(left));
        Assert.True(left == right);
        Assert.True(right == left);
        Assert.True(left == value);
        Assert.True(value == left);
    }

    [Fact]
    public void Operator_ShouldNot_Equal()
    {
        var value1 = 1;
        var value2 = 2;

        var left = EqualityTest.From(value1);
        var right = EqualityTest.From(value2);

        Assert.False(left.Equals(right));
        Assert.False(right.Equals(left));
        Assert.True(left != right);
        Assert.True(right != left);
        Assert.True(left != value2);
        Assert.True(value2 != left);
    }
}
