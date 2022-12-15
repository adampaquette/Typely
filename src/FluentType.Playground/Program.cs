
using System.Numerics;

var builder = ValueObjectBuilder.Instance;

builder.For<UserId>().As<int>().Length(1, 100);
//builder.MinLength<UserId>(1, 100);

builder.For<FirstName>()
    .As<string>()
    .Length(10, 100);

public partial record struct FirstName(string Value)
{
    public bool Equals(FirstName other)
    {
        return EqualityComparer<string>.Default.Equals(Value, other.Value);
    }
}

public readonly partial record struct FirstName;
public readonly partial record struct UserId;

public class ValueObjectBuilder
{
    private static readonly ValueObjectBuilder? _instance;

    private ValueObjectBuilder() { }

    public static ValueObjectBuilder Instance => _instance ?? new ValueObjectBuilder();
}

public static class ValueObjectBuilderExtensions
{
    public static ValueObjectBuilder<TValueObject> For<TValueObject>(this ValueObjectBuilder _) =>
        new ValueObjectBuilder<TValueObject>();
}

public class ValueObjectBuilder<TValueObject>
{
    public ValueObjectBuilder<TValueObject, TPrimitive> As<TPrimitive>() =>
        new ValueObjectBuilder<TValueObject, TPrimitive>();
}

public class ValueObjectBuilder<TValueObject, TPrimitive>
{
}

public static class ValueObjectNumberBuilder
{
    public static void Length<TValueObject, TPrimitive>(this ValueObjectBuilder<TValueObject, TPrimitive> builder, int minLength, int maxLength)
        where TPrimitive : INumber<TPrimitive>
    {
        throw new NotImplementedException();
    }

    public static void MaxLength<TValueObject, TPrimitive>(this ValueObjectBuilder<TValueObject, TPrimitive> builder, int maxLength)
        where TPrimitive : INumber<TPrimitive>
    {
        throw new NotImplementedException();
    }

    public static void Length<TValueObject>(this ValueObjectBuilder<TValueObject, string> builder, int minLength, int maxLength)
    {
        throw new NotImplementedException();
    }
}