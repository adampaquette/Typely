using System.Collections.Immutable;
using Typely.Generators.Comparers;

namespace Typely.Generators.Tests.Comparers;

public class DictionaryComparerTests
{
    [Fact]
    public void TwoDictionariesWithTheSameKeysAndValues_ShouldBe_Equal()
    {
        var dict1 = new Dictionary<string, object?> { { "MaxLength", "10" } }.ToImmutableDictionary();
        var dict2 = new Dictionary<string, object?> { { "MaxLength", "10" } }.ToImmutableDictionary();

        Assert.True(DictionaryComparer<string, object?>.Default.Equals(dict1, dict2));
        Assert.Equal(DictionaryComparer<string, object?>.Default.GetHashCode(dict1), DictionaryComparer<string, object?>.Default.GetHashCode(dict2));
    }
    
    [Fact]
    public void TwoDictionariesWithSameRef_ShouldBe_Equal()
    {
        var dict1 = new Dictionary<string, object?> { { "MaxLength", "10" } }.ToImmutableDictionary();

        Assert.True(DictionaryComparer<string, object?>.Default.Equals(dict1, dict1));
    }

    [Fact]
    public void TwoDictionariesWithKeyDifference_ShouldNotBe_Equal()
    {
        var dict1 =
            new Dictionary<string, object?> { { "MaxLength", "10" }, { "Prop1", "val1" } }.ToImmutableDictionary();
        var dict2 = new Dictionary<string, object?> { { "MaxLength", "10" } }.ToImmutableDictionary();

        Assert.False(DictionaryComparer<string, object?>.Default.Equals(dict1, dict2));
    }
    
    [Fact]
    public void TwoDictionariesWithTheSameNumberOfKeyButDiffValues_ShouldNotBe_Equal()
    {
        var dict1 = new Dictionary<string, object?> { { "MaxLength", "10" } }.ToImmutableDictionary();
        var dict2 = new Dictionary<string, object?> { { "Prop1", "10" } }.ToImmutableDictionary();

        Assert.False(DictionaryComparer<string, object?>.Default.Equals(dict1, dict2));
    }
    
    [Fact]
    public void TwoDictionariesWithTheSameKeysButDiffValues_ShouldNotBe_Equal()
    {
        var dict1 = new Dictionary<string, object?> { { "MaxLength", "10" } }.ToImmutableDictionary();
        var dict2 = new Dictionary<string, object?> { { "MaxLength", "11" } }.ToImmutableDictionary();

        Assert.False(DictionaryComparer<string, object?>.Default.Equals(dict1, dict2));
    }
    
    [Fact]
    public void TwoDictionariesWithOneNull_ShouldNotBe_Equal()
    {
        var dict1 = new Dictionary<string, object?> { { "MaxLength", "10" } }.ToImmutableDictionary();

        Assert.False(DictionaryComparer<string, object?>.Default.Equals(dict1, null));
        Assert.Equal(0, DictionaryComparer<string, object?>.Default.GetHashCode(null));
    }
}