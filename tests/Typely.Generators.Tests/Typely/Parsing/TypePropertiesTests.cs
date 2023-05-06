using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

public class TypePropertiesTests
{
    [Fact]
    public void TwoTypePropertiesWithSameMaxLengthAreEqual()
    {
        var typeProperties1 = new TypeProperties();
        typeProperties1.SetMaxLength(10);

        var typeProperties2 = new TypeProperties();
        typeProperties2.SetMaxLength(10);

        Assert.True(DictionaryComparer<string, string>.Default.Equals(typeProperties1, typeProperties2));
    }
}