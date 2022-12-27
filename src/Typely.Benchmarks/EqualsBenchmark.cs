using BenchmarkDotNet.Attributes;

namespace Typely.Benchmarks;

public class EqualsBenchmark
{
    [MemoryDiagnoser]
    public class IntTests
    {
        readonly int val1 = new Random().Next();
        readonly int val2 = new Random().Next();

        [Benchmark]
        public bool Int_EqualOperator() => val1 == val2;

        [Benchmark]
        public bool Int_EqualityComparer() => EqualityComparer<int>.Default.Equals(val1, val2);

        [Benchmark]
        public bool Int_Equals() => val1.Equals(val2);
    }

    [MemoryDiagnoser]
    public class StringTests
    {
        readonly string val1 = new Random().Next().ToString();
        readonly string val2 = new Random().Next().ToString();

        [Benchmark]
        public bool String_EqualOperator() => val1 == val2;

        [Benchmark]
        public bool String_EqualityComparer() => EqualityComparer<string>.Default.Equals(val1, val2);

        [Benchmark]
        public bool String_Equals() => val1.Equals(val2);

        [Benchmark]
        public bool String_StaticEquals() => string.Equals(val1, val2);
    }

    [MemoryDiagnoser]
    public class GuidTests
    {
        readonly Guid val1 = Guid.NewGuid();
        readonly Guid val2 = Guid.NewGuid();

        [Benchmark]
        public bool Guid_EqualOperator() => val1 == val2;

        [Benchmark]
        public bool Guid_EqualityComparer() => EqualityComparer<Guid>.Default.Equals(val1, val2);

        [Benchmark]
        public bool Guid_Equals() => val1.Equals(val2);
    }

    [MemoryDiagnoser]
    public class ValueObjectIntTests
    {
        public record struct FirstName_EqualityComparer(int Value)
        {
            public bool Equals(FirstName_EqualityComparer other) => EqualityComparer<int>.Default.Equals(Value, other.Value);
        }

        public record struct FirstName_Equals(int Value)
        {
            public bool Equals(FirstName_Equals other) => Value.Equals(other.Value);
        }

        readonly FirstName_EqualityComparer val1 = new FirstName_EqualityComparer(new Random().Next());
        readonly FirstName_EqualityComparer val2 = new FirstName_EqualityComparer(new Random().Next());
        readonly FirstName_Equals val3 = new FirstName_Equals(new Random().Next());
        readonly FirstName_Equals val4 = new FirstName_Equals(new Random().Next());

        [Benchmark]
        public bool ValueObjectInt_EqualityComparer() => val1.Equals(val2);

        [Benchmark]
        public bool ValueObjectInt_Equals() => val3.Equals(val4);
    }

    [MemoryDiagnoser]
    public class ValueObjectStringTests
    {
        public record struct FirstName_EqualityComparer(string Value)
        {
            public bool Equals(FirstName_EqualityComparer other) => EqualityComparer<string>.Default.Equals(Value, other.Value);
        }

        public record struct FirstName_Equals(string Value)
        {
            public bool Equals(FirstName_Equals other) => Value.Equals(other.Value);
        }

        readonly FirstName_EqualityComparer val1 = new FirstName_EqualityComparer(new Random().Next().ToString());
        readonly FirstName_EqualityComparer val2 = new FirstName_EqualityComparer(new Random().Next().ToString());
        readonly FirstName_Equals val3 = new FirstName_Equals(new Random().Next().ToString());
        readonly FirstName_Equals val4 = new FirstName_Equals(new Random().Next().ToString());

        [Benchmark]
        public bool ValueObjectString_EqualityComparer() => val1.Equals(val2);

        [Benchmark]
        public bool ValueObjectString_Equals() => val3.Equals(val4);
    }
}
