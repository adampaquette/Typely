using System.Collections.Immutable;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

public class EmittableTypeTests
{
    [Fact]
    public void TwoTypesWithTheSameProperties_ShouldBe_Equal()
    {
        var rules1 = new List<EmittableRule>
        {
            new("string", "Name", "Name",
                new Dictionary<string, string> { { "MaxLength", "10" } }.ToImmutableDictionary()),
        }.ToImmutableArray();
        var type1 = new EmittableType("underlyingType", true, "typeName", "name",
            "namespace", "configNamespace", ConstructTypeKind.Struct, null,
            rules1, new List<string> { "System" }.ToImmutableArray(), new TypeProperties());

        var rules2 = new List<EmittableRule>
        {
            new("string", "Name", "Name",
                new Dictionary<string, string> { { "MaxLength", "10" } }.ToImmutableDictionary()),
        }.ToImmutableArray();
        var type2 = new EmittableType("underlyingType", true, "typeName", "name",
            "namespace", "configNamespace", ConstructTypeKind.Struct, null,
            rules2, new List<string> { "System" }.ToImmutableArray(), new TypeProperties());

        Assert.True(type1.Equals(type2));
    }
}