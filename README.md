
# Example

```c#
public class MyDomainTypesConfiguration : IFluentTypesConfiguration
{
    public void Configure(FluentTypeBuilder builder)
    {
        builder.For<int>("Likes");
        builder.For<decimal>("Rating").InclusiveBetween(0, 5);
        builder.For<string>("FirstName").NotEmpty();
        builder
            .For<int>("UserId")
            .Namespace("MyDomain")
            .AsStruct()
            .Length(20)
            .WithMessage("Pleasy specify a {TypeName} with a length of {Length}.")
            .Matches("[0-9]{5}[a-Z]{15}");
            .WithName("user identifier");
    }
}

var firstName = FirstName.From("Adam");
var userId = UserId.From(-1); //Throws ArgumentException
```

# Built-in Validators

## NotEmpty Validator

## NotEqual Validator

## Length Validator

## MinLength Validator

## MaxLength Validator

## LessThan Validator

## LessThanOrEqual Validator

## GreaterThan Validator

## GreaterThanOrEqual Validator

## Must Validator

`Must`

## Regular Expression Validator

`Matches`

## InclusiveBetween Validator

## ExclusiveBetween Validator

## PrecisionScale Validator

# Why is there no implicit conversion?

Because it would remove the type safety brought by the value objects. Let's suppose the following scenario:
```c#
builder.For<decimal>("Cost");
builder.For<decimal>("Rating");

var cost = Cost.From(12.1);
var rating = Rating.From(4.8);

if(cost >= rating)
{
    // Compiled and did not throw 
}
```

# Benchmarks

|               Method |      Mean |     Error |    StdDev |    Median | Allocated |
|--------------------- |----------:|----------:|----------:|----------:|----------:|
|    Int_EqualOperator | 0.0028 ns | 0.0053 ns | 0.0050 ns | 0.0000 ns |         - |
| Int_EqualityComparer | 0.0018 ns | 0.0031 ns | 0.0027 ns | 0.0004 ns |         - |
|           Int_Equals | 0.0000 ns | 0.0001 ns | 0.0001 ns | 0.0000 ns |         - |

|                  Method |      Mean |     Error |    StdDev | Allocated |
|------------------------ |----------:|----------:|----------:|----------:|
|    String_EqualOperator | 2.4031 ns | 0.0227 ns | 0.0212 ns |         - |
| String_EqualityComparer | 5.0523 ns | 0.0305 ns | 0.0270 ns |         - |
|           String_Equals | 0.5165 ns | 0.0206 ns | 0.0182 ns |         - |
|     String_StaticEquals | 2.4197 ns | 0.0249 ns | 0.0208 ns |         - |

|                  Method |     Mean |     Error |    StdDev | Allocated |
|------------------------ |---------:|----------:|----------:|----------:|
|      Guid_EqualOperator | 1.608 ns | 0.0062 ns | 0.0052 ns |         - |
|   Guid_EqualityComparer | 1.659 ns | 0.0358 ns | 0.0335 ns |         - |
|             Guid_Equals | 1.334 ns | 0.0236 ns | 0.0197 ns |         - |

|                          Method |      Mean |     Error |    StdDev |    Median | Allocated |
|-------------------------------- |----------:|----------:|----------:|----------:|----------:|
| ValueObjectInt_EqualityComparer | 0.0013 ns | 0.0021 ns | 0.0018 ns | 0.0006 ns |         - |
|           ValueObjectInt_Equals | 0.0148 ns | 0.0115 ns | 0.0108 ns | 0.0133 ns |         - |

|                             Method |     Mean |     Error |    StdDev | Allocated |
|----------------------------------- |---------:|----------:|----------:|----------:|
| ValueObjectString_EqualityComparer | 5.228 ns | 0.0702 ns | 0.0657 ns |         - |
|           ValueObjectString_Equals | 2.643 ns | 0.0417 ns | 0.0370 ns |         - |


# VNext

```c#
builder.For<string>("Sexe")
    .In("Male", "Female")
    .Comparer(StringComparer.OrdinalIgnoreCase);
```