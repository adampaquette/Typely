using BenchmarkDotNet.Attributes;

namespace FluentType.Benchmarks;

public class EqualsBenchmark
{
    //[MemoryDiagnoser]
    //public class Int
    //{
    //    readonly int val1 = new Random().Next();
    //    readonly int val2 = new Random().Next();

    //    [Benchmark]
    //    public bool Int_EqualOperator() => val1 == val2;

    //    [Benchmark]
    //    public bool Int_EqualityComparer() => EqualityComparer<int>.Default.Equals(val1, val2);

    //    [Benchmark]
    //    public bool Int_Equals() => val1.Equals(val2);
    //}

    //[MemoryDiagnoser]
    //public class String
    //{
    //    readonly string val1 = "value1";
    //    readonly string val2 = "value2";

    //    [Benchmark]
    //    public bool String_EqualOperator() => val1 == val2;

    //    [Benchmark]
    //    public bool String_EqualityComparer() => EqualityComparer<string>.Default.Equals(val1, val2);

    //    [Benchmark]
    //    public bool String_Equals() => val1.Equals(val2);

    //    [Benchmark]
    //    public bool String_StaticEquals() => string.Equals(val1, val2);
    //}

    [MemoryDiagnoser]
    public class ValueObjectString
    {
        public record struct FirstName_EqualityComparer(string Value)
        {
            public bool Equals(FirstName_EqualityComparer other) => EqualityComparer<string>.Default.Equals(Value, other.Value);
        }

        public record struct FirstName_Equals(string Value)
        {
            public bool Equals(FirstName_Equals other) => Value.Equals(Value);
        }

        readonly FirstName_EqualityComparer val1 = new FirstName_EqualityComparer("value1");
        readonly FirstName_EqualityComparer val2 = new FirstName_EqualityComparer("value2");
        readonly FirstName_Equals val3 = new FirstName_Equals("value3");
        readonly FirstName_Equals val4 = new FirstName_Equals("value4");

        [Benchmark]
        public bool ValueObjectString_EqualityComparer() => val1.Equals(val2);

        [Benchmark]
        public bool ValueObjectString_Equals() => val3.Equals(val4);
    }
}
