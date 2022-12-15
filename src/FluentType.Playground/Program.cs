
using System.Numerics;

var builder = ValueObjectBuilder.Instance;

builder.For<UserId>().As<int>().Length(1, 100);

/*
Base validators:

builder.NotEmpty<UserId>();
builder.Length<UserId>(20, 250);
builder.MinLength<UserId>(3);
builder.MaxLength<UserId>(3);
builder.LessThen<UserId>(3);
builder.LessThanOrEqualTo<UserId>(3);
builder.GreaterThan<UserId>(3);
builder.GreaterThanOrEqualTo<UserId>(3);
builder.Must<UserId>(BeAValidId);
builder.Matches<UserId>("[0-9]{2}");
builder.Equal<UserId>("abc")
builder.NotEqual<UserId>("abc");
builder.NotEqual<UserId>("abc", StringComparer.OrdinalIgnoreCase);
builder.ExclusiveBetween<UserId>(1, 10);
builder.InclusiveBetween<UserId>(1, 10);
builder.PrecisionScale<UserId>(4, 2, false);
builder.IsInEnum<UserId>(MyEnum); Usefull ??

Combine validators:

builder
    .NotEmpty<UserId>()
    .Length(20)
    .Matches("[0-9]{20}");

Extend validators:

builder.MinLength<UserId>().WithMessage("Pleasy specify a {TypeName} with a minimum length of {MinLength}.");
builder.For<UserId>().As<int>().Must(BeAValidEmail).WithMessage("Please enter a valid email.");


Global configurations:

builder.Global.AllowNull();
builder.Global.AllowDefault();
*/

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