![Typely](assets/logo-300.png)

Typerly let you create types easyly with a fluent API to embrace Domain-driven design and value objects. 


# Example

```csharp
public class TypesConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.For<int>("Likes");
        builder.For<decimal>("Rating").InclusiveBetween(0, 5);
        builder.For<string>("FirstName").NotEmpty();
        
        builder
            .For<int>("UserId")
            .Namespace("MyDomain")
            .AsStruct()
            .Length(20)
            .WithMessage("Please specify a user id with a length of 20.")
            .Matches("[0-9]{5}[a-Z]{15}")
            .WithName("user identifier");

        builder
            .For<string>("Title")
            .Normalize(x => x.Replace(" ", "").Trim())            
            .NotEmpty().WithMessage(c => $"Please specify a {c.Name}.")
            .MaxLength(20).WithMessage(c => $"Please specify a {c.Name} with a max length of {c.MaxLength}.")
            .Must(x => !x.contains("custom"));
    }
}

var firstName = FirstName.From("Adam");
var userId = UserId.From(-1); //Throws ArgumentException
```

# Built-in Validators

## NotEmpty Validator

Ensures that the value is not null, an empty string or whitespace or the default value for value types, e.g., 0 for int.

Example:

```c#
builder.For<string>("Name").NotEmpty();
```
Example error: 'Name' must not be empty.

String format args:
- `Name`: Name of the type being validated
- `Value`: Current value of the property
___
## NotEqual Validator

Ensures that the value is not equal to a specified value.

Example:

```c#
builder.For<string>("Name").NotEqual("value");
```
Example error: 'Name' must not be empty.

String format args:
- `Name`: Name of the type being validated
- `Value`: Current value of the property
- `ComparisonValue`: Value that should not equal

Optionally, a comparer can be specified:
```c#
builder.For<string>("Name").NotEqual("value", StringComparer.OrdinalIgnoreCase);
```
___
## Length Validator

## MinLength Validator

## MaxLength Validator

## LessThan Validator  (Max?)

## LessThanOrEqual Validator

## GreaterThan Validator (Min?)

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
    // Compile and does not throw 
}
```

# Why do generated types have an interface?

- First, because our value objects need to be compile-time type safe, we can't use implicit conversions, so we need to have some way to access the underlying value.
- Second, to respect the open-close principle, we want developers to be able to add new functionality to their value object without having to modify the sources of the `Typely` generator.
- Third, the use of interface reduces the redundancy such as for example converters.

All the generated types implements `IValue`, giving access to a property named `Value`.

# Supported frameworks

- .NET 5.0 or greater are first class citizens frameworks
- Backward compatible with .NET Standard 2.0

Because I wanted the code base to be as simple as possible and extensible, I needed the value objects to be created generically for exemple in the converters.
As the feature for static method inside interfaces came with C# 8.0, it could not be part of .NET Standard 2.0. The trade off here is that projects targeting .NET 5.0 or greater will be first class citizens and projects using .NET Standard 2.0 will benefits from all the same features but using reflexion where generic static method could not be used.
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/default-interface-methods
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/configure-language-version
https://learn.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-1#when-to-target-net50-or-net60-vs-netstandard

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


# Limitations

- Classes implementing `ITypelyConfiguration` should have explicit references for references outside of following list:
    - System
    - System.Collections.Generic
    - System.IO
    - System.Linq
    - System.Net.Http
    - System.Threading
    - System.Threading.Tasks

# VNext

```c#
// Localization
builder.Localization
    .Enable()
    .Disable()
    .Culture(new CultureInfo("fr-CA"));

// Converters
// Requires to load dependencies of the SyntaxTree
builder.Converters
    .Clear()
    .Add(new JsonConverterAttribute(typeof(TypelyJsonConverter<LastName, string>)))
    .AddSystemTextJsonConverter()
    .AddNewtonsoftJsonConverter()
    .AddTypeConverter();

// Creating a class with default configurations.
// Requires the generator to recursively detect the interface `ITypelyConfiguration`
public class DefaultTypelyConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.Converters
            .AddFromDetectedDepencencies()
            .AddSystemTextJsonConverter()
            .AddTypeConverter();
    }
}

// Enums
builder.For<string>("Sexe")
    .In("Male", "Female")
    .Comparer(StringComparer.OrdinalIgnoreCase);

builder.For<string>("Sexe")
    .In((1, "Male"), (2, "Female"));

// Typed factory
var factory = builder.Factory<string>()
    .Namespace("MyDomain")
    .AsStruct()
    .Length(3, 50)
    .Build();
    
factory.For("FirstName");
factory.For("LastName");

// Untyped factory
var factory2 = builder.Factory()
    .Namespace("MyDomain")
    .NotEmpty()
    .Build();
    
factory2.For<int>("Days");
factory2.For<long>("Timespan");

// Attributes
builder.Attributes
    .Clear()
    .Add(new JsonConverterAttribute(typeof(TypelyJsonConverter<LastName, string>)));
```