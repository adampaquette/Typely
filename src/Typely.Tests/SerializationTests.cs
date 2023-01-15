namespace Typely.Tests;

public class SerializationTests
{
    internal class BasicClass
    {
        public SerializationTestsType SerializationTestsType { get; set; }
    }

    [Fact]
    public void SystemTextJson_Should_Serialize()
    {
        var obj = new BasicClass
        {
            SerializationTestsType = SerializationTestsType.From("Adam")
        };

        var actual = System.Text.Json.JsonSerializer.Serialize(obj);

        Assert.Equal("{\"SerializationTestsType\":\"Adam\"}", actual);
    }

    [Fact]
    public void SystemTextJson_Should_Deserialize()
    {
        var expected = new BasicClass
        {
            SerializationTestsType = SerializationTestsType.From("Adam")
        };

        var obj = "{\"SerializationTestsType\":\"Adam\"}";

        var actual = System.Text.Json.JsonSerializer.Deserialize<BasicClass>(obj);

        Assert.Equal(expected.SerializationTestsType, actual!.SerializationTestsType);
    }

    //[Fact]
    //public void Newtonsoft_Json_Should_Serialize()
    //{
    //    var obj = new BasicClass
    //    {
    //        SerializationTestsType = SerializationTestsType.From("Adam")
    //    };

    //    var actual = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

    //    Assert.Equal("{\"SerializationTestsType\":\"Adam\"}", actual);
    //}

    //[Fact]
    //public void Newtonsoft_Json_Should_Deserialize()
    //{
    //    var expected = new BasicClass
    //    {
    //        SerializationTestsType = SerializationTestsType.From("Adam")
    //    };

    //    var obj = "{\"SerializationTestsType\":\"Adam\"}";

    //    var actual = Newtonsoft.Json.JsonConvert.DeserializeObject<BasicClass>(obj);

    //    Assert.Equal(expected, actual);
    //}
}