using System.Text.Json;

namespace Typely.Tests.Converters;

public class TypelyJsonConverterTests
{
    internal class BasicClass
    {
        public SerializationTestsType Prop { get; set; }
    }

    [Fact]
    public void SystemTextJson_Should_Serialize()
    {
        var obj = new BasicClass
        {
            Prop = SerializationTestsType.From("Adam")
        };

        var actual = JsonSerializer.Serialize(obj);

        Assert.Equal("{\"Prop\":\"Adam\"}", actual);
    }

    [Fact]
    public void SystemTextJson_Should_Deserialize()
    {
        var expected = new BasicClass
        {
            Prop = SerializationTestsType.From("Adam")
        };

        var obj = "{\"Prop\":\"Adam\"}";

        var actual = JsonSerializer.Deserialize<BasicClass>(obj);

        Assert.Equal(expected.Prop, actual!.Prop);
    }

    [Fact]
    public void SystemTextJson_Should_DeserializeEmpty()
    {
        var obj = "{\"Prop\":null}";

        var actual = JsonSerializer.Deserialize<BasicClass>(obj)!;

        Assert.Null(actual.Prop.Value);
    }
}
