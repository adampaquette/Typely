using System.Text.Json;

namespace Typely.Tests;

public class Tests
{
    [Fact(Skip = "debug")]
    public void BadLanguageConstructs()
    {
        var a = new ReferenceSample();
        var c = default(ReferenceSample);
    }

    [Fact(Skip = "debug")]
    public void TestReferenceSample()
    {
        var a = ReferenceSample.From(1);
        var b = new ReferenceSample(2);

        var aa = (int)a;
        var areEqual1 = a == b;
        var areEqual2 = a != b;
        //var areEqual3 = a == 1;
        //var areEqual4 = a != 1;
        //var areEqual5 = 1 == a;
        //var areEqual6 = 1 != a;


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