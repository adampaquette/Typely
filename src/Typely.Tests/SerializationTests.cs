namespace Typely.Tests;

public class SerializationTests
{
    [Fact]
    public void SystemTextJson_Should_Serialize()
    {
        var obj = new BasicClass
        {
            FirstName = new FirstName
            {
                Value = "Adam"
            }
        };

        var actual = System.Text.Json.JsonSerializer.Serialize(obj);

        Assert.Equal("{\"FirstName\":\"Adam\"}", actual);
    }

    [Fact]
    public void SystemTextJson_Should_Deserialize()
    {
        var expected = new BasicClass
        {
            FirstName = new FirstName
            {
                Value = "Adam"
            }
        };

        var obj = "{\"FirstName\":\"Adam\"}";

        var actual = System.Text.Json.JsonSerializer.Deserialize<BasicClass>(obj);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Newtonsoft_Json_Should_Serialize()
    {
        var obj = new BasicClass
        {
            FirstName = new FirstName
            {
                Value = "Adam"
            }
        };

        var actual = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

        Assert.Equal("{\"FirstName\":\"Adam\"}", actual);
    }

    [Fact]
    public void Newtonsoft_Json_Should_Deserialize()
    {
        var expected = new BasicClass
        {
            FirstName = new FirstName
            {
                Value = "Adam"
            }
        };

        var obj = "{\"FirstName\":\"Adam\"}";

        var actual = Newtonsoft.Json.JsonConvert.DeserializeObject<BasicClass>(obj);

        Assert.Equal(expected, actual);
    }
}