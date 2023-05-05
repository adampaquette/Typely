using System.Collections.Immutable;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

public class EmittableTypeTests
{
    [Fact]
    public void TwoTypesWithTheSameProperties_ShouldBe_Equal()
    {
        var rules = new List<EmittableRule>
        {
            new("string", "Name", "Name", new Dictionary<string, string> { { "MaxLength", "10" } })
        }.ToImmutableArray();

        var type1 = new EmittableType("underlyingType", true, "typeName", "name",
            "namespace", "configNamespace", ConstructTypeKind.Struct, null,
            rules, new List<string> { "System" }.ToImmutableArray(), new TypeProperties());

        var type2 = new EmittableType("underlyingType", true, "typeName", "name",
            "namespace", "configNamespace", ConstructTypeKind.Struct, null,
            rules, new List<string> { "System" }.ToImmutableArray(), new TypeProperties());

        Assert.Equal(type1, type2);
    }
}