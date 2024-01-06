using BenchmarkDotNet.Attributes;

namespace Typely.Benchmarks;

[MemoryDiagnoser]
public class ClassVsStructBenchmark
{
    private string _text = string.Empty;
    
    [Params(10, 100)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        var randomString = new RandomString();
        _text = randomString.Next(N);
    }

    [Benchmark]
    public void Struct() =>  new NameStruct(_text);

    [Benchmark]
    public void Class() => new NameClass(_text);
}

public class NameClass
{
    public string Value { get; }

    public NameClass(string value)
    {
        Value = value;
    }
}

public struct NameStruct
{
    public string Value { get; }

    public NameStruct(string value)
    {
        Value = value;
    }
}