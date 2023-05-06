using System.Collections.Immutable;
using Typely.Generators.Comparers;

namespace Typely.Generators.Tests.Comparers;

public class ImmutableArrayComparerTests
{
    [Fact]
    public void TwoArray_WithTheSameValues_ShouldBe_Equal()
    {
        var array1 = new[] { "Value1", "Value2" }.ToImmutableArray();
        var array2 = new[] { "Value1", "Value2" }.ToImmutableArray();

        Assert.True(ImmutableArrayComparer<string>.Default.Equals(array1, array2));
        Assert.Equal(ImmutableArrayComparer<string>.Default.GetHashCode(array1), ImmutableArrayComparer<string>.Default.GetHashCode(array2));
    }
    
    [Fact]
    public void TwoArray_WithDiffLength_ShouldNotBe_Equal()
    {
        var array1 = new[] { "Value1", "Value2" }.ToImmutableArray();
        var array2 = new[] { "Value1" }.ToImmutableArray();

        Assert.False(ImmutableArrayComparer<string>.Default.Equals(array1, array2));
    }
}