namespace Typely.Tests;

public class SerializationTests
{
    [Fact]
    public void SystemTextJson_Should_Serialize()
    {
        var firstName = new FirstName
        {
            Value = "Adam"
        };

        var actual = System.Text.Json.JsonSerializer.Serialize(firstName);

        Assert.Equal("{\"Value\":\"Adam\"}", actual);
    }

    [Fact]
    public void SystemTextJson_Should_Deserialize()
    {
        var expected = new FirstName
        {
            Value = "Adam"
        };

        var firstName = "{\"Value\":\"Adam\"}";

        var actual = System.Text.Json.JsonSerializer.Deserialize<FirstName>(firstName);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Newtonsoft_Json_Should_Serialize()
    {
        var firstName = new FirstName
        {
            Value = "Adam"
        };

        var actual = Newtonsoft.Json.JsonConvert.SerializeObject(firstName);

        Assert.Equal("{\"Value\":\"Adam\"}", actual);
    }

    [Fact]
    public void Newtonsoft_Json_Should_Deserialize()
    {
        var expected = new FirstName
        {
            Value = "Adam"
        };

        var firstName = "{\"Value\":\"Adam\"}";

        var actual = Newtonsoft.Json.JsonConvert.DeserializeObject<FirstName>(firstName);

        Assert.Equal(expected, actual);
    }
}