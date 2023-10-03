using BenchmarkDotNet.Attributes;
using Typely.Core;
using Typely.Core.Builders;
using ValueOf;
using Vogen;

namespace Typely.Benchmarks;

public class ValueObjectLibrariesBenchmark
{
    [MemoryDiagnoser]
    public class Int
    {
        [Benchmark]
        public void IntCreation_Typely()
        {
            TypelyInt.From(1);
        }

        [Benchmark]
        public void IntCreation_ValueOf()
        {
            ValueOfInt.From(1);
        }

        [Benchmark]
        public void IntCreation_Vogen()
        {
            VogenInt.From(1);
        }
        
        [Benchmark]
        public void IntCreation_Primitive()
        {
            Validate(1);
        }

        private static void Validate(int value)
        {
            if (value < 0)
            {
                throw new Exception();
            }
        }
    }
}

public class ValueOfInt : ValueOf<int, ValueOfInt>
{
    protected override void Validate()
    {
        if (Value < 0)
        {
            throw new Exception("Must be greater than zero.");
        }
    }
}

[ValueObject<int>]
public partial struct VogenInt
{
    public static Validation Validate(int value) =>
        value > 0 ? Validation.Ok : Validation.Invalid("Must be greater than zero.");
}

public class TypelySpec : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        builder.OfInt().For("TypelyInt").GreaterThan(0);
        builder.OfInt().For("TypelyInt2").GreaterThan(0);
    }
}