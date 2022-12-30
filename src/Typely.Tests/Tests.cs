namespace Typely.Tests;

public class Tests
{
    public void BadLanguageConstructs()
    {
        var a = new MyStruct();
        var b = a with { Value = 8 };
        var c = default(MyStruct);
    }

    public void TestReferenceSample()
    {
        var a = ReferenceSample.From(1);
        var b = new ReferenceSample(2);
        ReferenceSample.TryFrom(1, out var i, out var e);
        ReferenceSample.Validate(-1);
    }
}

public struct MyStruct
{
    public int Value { get; set; }
}