namespace Typely.Tests;

public class Tests
{
    [Fact]
    public void BadLanguageConstructs()
    {
        var a = new MyStruct();
        var b = a with { Value = 8 };
        var c = default(MyStruct);
    }
}

public struct MyStruct
{
    public int Value { get; set; }
}