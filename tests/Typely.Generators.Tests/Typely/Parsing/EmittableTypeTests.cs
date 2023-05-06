using System.Collections.Immutable;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

public class EmittableTypeTests
{
    [Fact]
    public void TwoTypesWithTheSameProperties_ShouldBe_Equal()
    {
        var properties1 = new TypeProperties();
        properties1.SetMaxLength(10);
        var rules1 = new List<EmittableRule>
        {
            new("string", "Name", "Name",
                new Dictionary<string, object?> { { "MaxLength", "10" } }.ToImmutableDictionary()),
        }.ToImmutableArray();
        var type1 = new EmittableType("underlyingType", true, "typeName", "name",
            "namespace", "configNamespace", ConstructTypeKind.Struct, null,
            rules1, new List<string> { "System" }.ToImmutableArray(), properties1);

        var properties2 = new TypeProperties();
        properties2.SetMaxLength(10);
        var rules2 = new List<EmittableRule>
        {
            new("string", "Name", "Name",
                new Dictionary<string, object?> { { "MaxLength", "10" } }.ToImmutableDictionary()),
        }.ToImmutableArray();
        var type2 = new EmittableType("underlyingType", true, "typeName", "name",
            "namespace", "configNamespace", ConstructTypeKind.Struct, null,
            rules2, new List<string> { "System" }.ToImmutableArray(), properties2);

        Assert.True(type1.Equals(type2));
        Assert.Equal(type1.GetHashCode(), type2.GetHashCode());
    }
    
    [Fact]
    public void TwoTypesWithEqualsButWithOneAddedProperty_ShouldNotBe_Equal()
    {
        var properties1 = new TypeProperties();
        properties1.SetMaxLength(10);
        var rules1 = new List<EmittableRule>
        {
            new("string", "Name", "Name",
                new Dictionary<string, object?> { { "MaxLength", "10" } }.ToImmutableDictionary()),
        }.ToImmutableArray();
        var type1 = new EmittableType("underlyingType", true, "typeName", "name",
            "namespace", "configNamespace", ConstructTypeKind.Struct, null,
            rules1, new List<string> { "System" }.ToImmutableArray(), properties1);

        var properties2 = new TypeProperties();
        properties2.SetMaxLength(10);
        var rules2 = new List<EmittableRule>
        {
            new("string", "Name", "Name",
                new Dictionary<string, object?> { { "MaxLength", "10" }, { "Prop2", "val2"} }.ToImmutableDictionary()),
        }.ToImmutableArray();
        var type2 = new EmittableType("underlyingType", true, "typeName", "name",
            "namespace", "configNamespace", ConstructTypeKind.Struct, null,
            rules2, new List<string> { "System", "Added" }.ToImmutableArray(), properties2);

        Assert.False(type1.Equals(type2));
    }
    
    [Fact]
    public void TwoTypesWithOneAnOtherType_ShouldNotBe_Equal()
    {
        var properties1 = new TypeProperties();
        properties1.SetMaxLength(10);
        var rules1 = new List<EmittableRule>
        {
            new("string", "Name", "Name",
                new Dictionary<string, object?> { { "MaxLength", "10" } }.ToImmutableDictionary()),
        }.ToImmutableArray();
        var type1 = new EmittableType("underlyingType", true, "typeName", "name",
            "namespace", "configNamespace", ConstructTypeKind.Struct, null,
            rules1, new List<string> { "System" }.ToImmutableArray(), properties1);
        
        // ReSharper disable once SuspiciousTypeConversion.Global
        Assert.False(type1.Equals("Value"));
    }
    
    [Fact]
    public void TwoTypesWithSameRef_ShouldBe_Equal()
    {
        var properties1 = new TypeProperties();
        properties1.SetMaxLength(10);
        var rules1 = new List<EmittableRule>
        {
            new("string", "Name", "Name",
                new Dictionary<string, object?> { { "MaxLength", "10" } }.ToImmutableDictionary()),
        }.ToImmutableArray();
        var type1 = new EmittableType("underlyingType", true, "typeName", "name",
            "namespace", "configNamespace", ConstructTypeKind.Struct, null,
            rules1, new List<string> { "System" }.ToImmutableArray(), properties1);
        
        Assert.True(type1.Equals(type1));
    }
}