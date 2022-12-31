using System.Text.Json;

namespace Typely.Tests;

public class Tests
{
    [Fact(Skip = "debug")]
    public void BadLanguageConstructs()
    {
        var a = new MyStruct();
        var b = a with { Value = 8 };
        var c = default(MyStruct);
    }

    [Fact(Skip = "debug")]
    public void TestReferenceSample()
    {
        var a = ReferenceSample.From(1);
        var b = new ReferenceSample(2);
        ReferenceSample.TryFrom(1, out var i, out var e);
        ReferenceSample.Validate(-1);

        var c = JsonSerializer.Serialize(a);
        try
        {
            var d = JsonSerializer.Deserialize<ReferenceSample>("-1");
        }
        catch (Exception ex)
        {
            var f = ex.ToString();
        }
    }
}

public struct MyStruct
{
    public int Value { get; set; }
}