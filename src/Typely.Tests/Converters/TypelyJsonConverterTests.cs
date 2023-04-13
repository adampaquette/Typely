using CsCheck;
using System.Text.Json;

namespace Typely.Tests.Converters;

public class TypelyJsonConverterTests
{
    internal class BasicClass
    {
        public IntSerializationTestsType Prop { get; set; }
    }

    [Fact]
    public void SystemTextJson_Serialize_RoundTrip() =>
        Gen.Int.Sample(x =>
        {           
            var obj = IntSerializationTestsType.From(x);
            var serialized = JsonSerializer.Serialize(obj);
            return JsonSerializer.Deserialize<IntSerializationTestsType>(serialized).Equals(obj);
        });

    // [Fact]
    // public void SystemTextJson_Should_DeserializeEmpty()
    // {
    //     var obj = "{\"Prop\":null}";
    //
    //     var actual = JsonSerializer.Deserialize<BasicClass>(obj)!;
    //
    //     Assert.Null(actual.Prop.Value);
    // }
}
