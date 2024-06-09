using BenchmarkDotNet.Attributes;

namespace Typely.Benchmarks;

[MemoryDiagnoser]
public class ReadonlyStructBenchmark
{
    private struct NumberStruct(int value)
    {
        public int Value { get; } = value;

        public override string ToString() => Value.ToString();
    }

    private readonly struct NumberReadonlyStruct(int value)
    {
        public int Value { get; } = value;

        public override string ToString() => Value.ToString();
    }

    private readonly NumberStruct _numberStruct = new(1);
    private readonly NumberReadonlyStruct _numberReadonlyStruct = new(1);

    [Benchmark]
    public string Struct()
    { 
        // Making a defensive copy of the value
        return _numberStruct.ToString();
    }

    [Benchmark]
    public string ReadOnlyStruct()
    {
        return _numberReadonlyStruct.ToString();
    }
}