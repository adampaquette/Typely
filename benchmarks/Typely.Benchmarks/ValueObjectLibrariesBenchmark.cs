using BenchmarkDotNet.Attributes;
using System.Text.Json;
using Typely.Core;
using Typely.Core.Builders;
using ValueOf;
using Vogen;

namespace Typely.Benchmarks;

public class ValueObjectLibrariesBenchmark
{
    [MemoryDiagnoser]
    public class IntRead
    {
        private TypelyInt typelyInt = TypelyInt.From(1);
        private ValueOfInt valueOfInt = ValueOfInt.From(1);
        private VogenInt vogenInt = VogenInt.From(1);
        private int primitiveInt = 1;

        [Benchmark]
        public void IntRead_Typely()
        {
            int i = typelyInt.Value;
        }

        [Benchmark]
        public void IntRead_ValueOf()
        {
            int i = valueOfInt.Value;
        }

        [Benchmark]
        public void IntRead_Vogen()
        {
            int i = vogenInt.Value;
        }

        [Benchmark]
        public void IntRead_Primitive()
        {
            int i = primitiveInt;
        }
    }

    [MemoryDiagnoser]
    public class IntCreation
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

    [MemoryDiagnoser]
    public class IntSerialization
    {
        private static TypelyInt typelyInt = TypelyInt.From(1);
        private static ValueOfInt valueOfInt = ValueOfInt.From(1);
        private static VogenInt vogenInt = VogenInt.From(1);
        private static int primitiveInt = 1;

        [Benchmark]
        public void IntSerialization_Typely()
        {
            var json = JsonSerializer.Serialize(typelyInt);
            var deserialized = JsonSerializer.Deserialize<TypelyInt>(json);
        }

        [Benchmark]
        public void IntSerialization_ValueOf()
        {
            var json = JsonSerializer.Serialize(valueOfInt);
            var deserialized = JsonSerializer.Deserialize<ValueOfInt>(json);
        }

        [Benchmark]
        public void IntSerialization_Vogen()
        {
            var json = JsonSerializer.Serialize(vogenInt);
            var deserialized = JsonSerializer.Deserialize<VogenInt>(json);
        }

        [Benchmark]
        public void IntSerialization_Primitive()
        {
            var json = JsonSerializer.Serialize(primitiveInt);
            var deserialized = JsonSerializer.Deserialize<int>(json);
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
    }
}