using BenchmarkDotNet.Attributes;

namespace Typely.Benchmarks;

public class EqualsBenchmark
{
    [MemoryDiagnoser]
    public class IntTests
    {
        readonly int _val1 = new Random().Next();
        readonly int _val2 = new Random().Next();

        [Benchmark]
        public bool Int_EqualOperator() => _val1 == _val2;

        [Benchmark]
        public bool Int_EqualityComparer() => EqualityComparer<int>.Default.Equals(_val1, _val2);

        [Benchmark]
        public bool Int_Equals() => _val1.Equals(_val2);
    }

    [MemoryDiagnoser]
    public class StringTests
    {
        readonly string _val1 = new Random().Next().ToString();
        readonly string _val2 = new Random().Next().ToString();

        [Benchmark]
        public bool String_EqualOperator() => _val1 == _val2;

        [Benchmark]
        public bool String_EqualityComparer() => EqualityComparer<string>.Default.Equals(_val1, _val2);

        [Benchmark]
        public bool String_Equals() => _val1.Equals(_val2);

        [Benchmark]
        public bool String_StaticEquals() => string.Equals(_val1, _val2);
    }

    [MemoryDiagnoser]
    public class GuidTests
    {
        readonly Guid _val1 = Guid.NewGuid();
        readonly Guid _val2 = Guid.NewGuid();

        [Benchmark]
        public bool Guid_EqualOperator() => _val1 == _val2;

        [Benchmark]
        public bool Guid_EqualityComparer() => EqualityComparer<Guid>.Default.Equals(_val1, _val2);

        [Benchmark]
        public bool Guid_Equals() => _val1.Equals(_val2);
    }

    [MemoryDiagnoser]
    public class ValueObjectIntTests
    {
        public record struct FirstNameEqualityComparer(int Value)
        {
            public bool Equals(FirstNameEqualityComparer other) => EqualityComparer<int>.Default.Equals(Value, other.Value);
            public override int GetHashCode() => Value.GetHashCode();
        }

        public record struct FirstNameEquals(int Value)
        {
            public bool Equals(FirstNameEquals other) => Value.Equals(other.Value);
            public override int GetHashCode() => Value.GetHashCode();            
        }

        readonly FirstNameEqualityComparer _val1 = new(new Random().Next());
        readonly FirstNameEqualityComparer _val2 = new(new Random().Next());
        readonly FirstNameEquals _val3 = new(new Random().Next());
        readonly FirstNameEquals _val4 = new(new Random().Next());

        [Benchmark]
        public bool ValueObjectInt_EqualityComparer() => _val1.Equals(_val2);

        [Benchmark]
        public bool ValueObjectInt_Equals() => _val3.Equals(_val4);
    }

    [MemoryDiagnoser]
    public class ValueObjectStringTests
    {
        public record struct FirstNameEqualityComparer(string Value)
        {
            public bool Equals(FirstNameEqualityComparer other) => EqualityComparer<string>.Default.Equals(Value, other.Value);
            public override int GetHashCode() => Value.GetHashCode();
        }

        public record struct FirstNameEquals(string Value)
        {
            public bool Equals(FirstNameEquals other) => Value.Equals(other.Value);
            public override int GetHashCode() => Value.GetHashCode();
        }

        readonly FirstNameEqualityComparer _val1 = new(new Random().Next().ToString());
        readonly FirstNameEqualityComparer _val2 = new(new Random().Next().ToString());
        readonly FirstNameEquals _val3 = new(new Random().Next().ToString());
        readonly FirstNameEquals _val4 = new(new Random().Next().ToString());

        [Benchmark]
        public bool ValueObjectString_EqualityComparer() => _val1.Equals(_val2);

        [Benchmark]
        public bool ValueObjectString_Equals() => _val3.Equals(_val4);
    }
}
