![Typely](assets/logo-300.png)

Typely let you create types easyly with a fluent API to embrace Domain-driven design and value objects. 

# Example

```csharp
public class TypesConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.For<int>("Likes");

        builder
            .For<int>("UserId")
            .Namespace("UserAggregate")
            .Name("Owner identifier")
            .AsStruct()
            .NotEmpty().WithMessage("'{Name}' cannot be empty.").WithErrorCode("ERR001")
            .NotEqual(1);

        builder.For<string>("Planet")
            .Name(() => Names.Planet)
            .NotEqual("sun").WithMessage(() => ErrorMessages.NotEqual)
    }
}

var likes = Likes.From(1365);
```

# Why Typely?

<!-- `Typely` brings validations to immutable value objects. With it's fluent Api, it let developpers enforce a set of rules witch defines a domain object that can only live if it's valid. This concept let's you build systems secure by design because you know that if an object is filled, it will be valid. Validations will also protect your system from potential vectors of attacks like Sql injection. 

You can see records as syntaxic sugar over class and struct. -->

- The entire boiler plate is generated for you. Example [here](./src/Typely.Generators.Tests/Typely/Snapshots/TypelyGeneratorSnapshotTests.Complete%23UserId.g.verified.cs).



# Built-in Validators

The only rule enforced by `Typely` is that the value object cannot be created with a null value when the underlying type is reference type. In this case, an `ArgumentNullException` will be thrown. Default values are still allowed like 0 for int or "" for string. If you want the type to be set to a null value, make it nullable (`MyType?`) or make the type a reference type with `AsClass()`.

## NotEmpty Validator

Ensures that the value is not an empty string or whitespace or the default value for value types, e.g., 0 for int.

Example:

```c#
builder.For<string>("Name").NotEmpty();
```
Example error: 'Name' must not be empty.

String format args:
- `Name`: Name of the type being validated
- `Value`: Current value of the property

<br>

## NotEqual Validator

Ensures that the value is not equal to a specified value.

Example:

```c#
builder.For<string>("Name").NotEqual("value");
```
Example error: 'Name' should not be equal to 'value'.

String format args:
- `Name`: Name of the type being validated
- `Value`: Current value of the property
- `ComparisonValue`: Value that should not equal

Optionally, a comparer can be specified:
```c#
builder.For<string>("Name").NotEqual("value", StringComparer.OrdinalIgnoreCase);
```

<br>

## Length Validator

Ensures that the length of a string is within the specified range.

Example:

```c#
builder.For<string>("Name").Length(1, 50);
```
Example error: 'Name' must be between 1 and 50 characters but currently has 51.

String format args:
- `Name`: Name of the type being validated
- `Value`: Current value of the property
- `MinLength`: Minimum length
- `MaxLength`: Maximum length
- `ActualLength`: Actual number of characters

<br>

## Exact Length Validator

Ensures that the length of a string is exactly the specified length.

Example:

```c#
builder.For<string>("Code").Length(10);
```
Example error: 'Code' must contain exactly 10 characters but currently has 11.

String format args:
- `Name`: Name of the type being validated
- `Value`: Current value of the property
- `ExactLength`: Exact length
- `ActualLength`: Actual number of characters

<br>

## MinLength Validator

Ensures that the length of a string is higher or equal to the specified value.

Example:

```c#
builder.For<string>("Name").MinLength(10);
```
Example error: 'Name' must be at least 10 characters but currently has 4.

String format args:
- `Name`: Name of the type being validated
- `Value`: Current value of the property
- `MinLength`: Minimum length
- `ActualLength`: Actual number of characters

<br>

## MaxLength Validator

Ensures that the length of a string is less than or equal to the specified value.

Example:

```c#
builder.For<string>("Name").MaxLength(50);
```
Example error: 'Name' must be 50 characters or less but currently has 51.

String format args:
- `Name`: Name of the type being validated
- `Value`: Current value of the property
- `MaxLength`: Maximum length
- `ActualLength`: Actual number of characters

<br>

## LessThan Validator  (Max?)

<br>

## LessThanOrEqual Validator

<br>

## GreaterThan Validator (Min?)

<br>

## GreaterThanOrEqual Validator

<br>

## Must Validator

`Must`

<br>

## Regular Expression Validator

`Matches`

<br>

## InclusiveBetween Validator

<br>

## ExclusiveBetween Validator

<br>

## PrecisionScale Validator

<br>

# Supported frameworks

- .NET 7.0 or greater are first class citizens frameworks
- Backward compatible with .NET Standard 2.0

Because I wanted the code base to be as simple as possible and extensible, I needed the value objects to be created generically for exemple in the converters. As the requierd feature for static methods on interfaces without implementation came with C# 11, it could not be part of .NET Standard 2.0. The trade off here is that projects targeting .NET 7.0 or greater will be optimal and projects using .NET Standard 2.0 will benefits from all the same features but using reflexion where generic static method could not be used.

https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-11.0/static-abstracts-in-interfaces
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/configure-language-version

# Class VS Struct

Theses are the 2 main constructs to create de type. A `class` witch is a reference type, will be allocated on the heap and garbage-collected whereas a `struct`, a value type, will be allocated on the stack or inlined in the containing type then deallocated simply by moving the stackpointer back to the previous fonction or when their containing type gets deallocated. Therefore memory management of value types will generally be less expensive.

C# 9.0 introduced `record` types to the language and provides an easy way to declare reference types with equality based on immutable values. C# 10 came with `record structs` to declare value types witch have the same benefits of traditionnal records. Under the hood, records generates `struct` or `class` with an implementation of IEquatable<T> and operators `==`, `!=`. 

So because `record` is just syntactic sugar, `Typely` won't use it but offers the same fonctionnality of creating a value type or a reference type like so:

```c#
builder.For<decimal>("Cost"); // By default, generated types will be `struct`.
builder.For<string>("Description").AsClass(); // Makes a reference type.
builder.For<decimal>("Rating").AsStruct(); // For complexe inheritance configurations, you can revert to the default `struct`.
```

https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/choosing-between-class-and-struct
https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#record-types
https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#record-structs

# Why is there no implicit conversion?

Because it would remove the type safety brought by the value objects. Let's suppose the following scenario:
```c#
builder.For<decimal>("Cost");
builder.For<decimal>("Rating");

var cost = Cost.From(12.1);
var rating = Rating.From(4.8);

if(cost >= rating)
{
    // Compiles and does not throw 
}
```

The preceding code compiles and does not throw. Also, a conversion that would result in an `InvalidCastException` should not be implicit. So it is not allowed to cast from a primitive to a value object but casting a value object to a primitive is always valid, so an explicit cast is implemented.

# Why do generated types have an interface?

- First, because our value objects need to be compile-time type safe, we can't use implicit conversions, so we need to have some way to access the underlying value.
- Second, to respect the open-close principle, we want developers to be able to add new functionality to their value object without having to modify the sources of the `Typely` generator.
- Third, the use of interface reduces the redundancy such as for example converters.

# Validations

Throwing and catching exceptions are very costly so `Typely` implements validations using the class `ValidationError`. To avoid extra memory allocation, if all validations passes, the object returned by the `Validate` method will be `null`. 

Note that you should use the method `TryFrom` to perform validation without throwing exception where possible otherwise `From`.

|                Method |         Mean |      Error |     StdDev |   Gen0 | Allocated |
|---------------------- |-------------:|-----------:|-----------:|-------:|----------:|
|       ReturnException | 5,407.274 ns | 28.0004 ns | 26.1916 ns | 0.0763 |     240 B |
| ReturnValidationError |     9.879 ns |  0.1042 ns |  0.0924 ns | 0.0153 |      48 B |

## Async validations

The validation state of a value object should not be dependant of any external state or services and thus calling async validations are not supported.

## Localization

You can localize validation messages using a lambda expression and a .resx file containing all translations. The body of the lambda will be built into the type itself and executed at run time.

```c#
builder.For<string>("Planet")
    .Name(() => Names.Planet)
    .NotEqual("sun").WithMessage(() => ErrorMessages.NotEqual);
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


# Limitations

- Classes implementing `ITypelyConfiguration` should have explicit references for references outside of the following list:
    - System
    - System.Collections.Generic
    - System.IO
    - System.Linq
    - System.Net.Http
    - System.Threading
    - System.Threading.Tasks

# VNext

```c#
- LessThan(T value); //IComparable
- LessThanOrEqual(T value); //IComparable
- GreaterThan(T value); //IComparable
- GreaterThanOrEqual(T value); //IComparable    
- InclusiveBetween(T min, T max); //INumber
    builder.For<decimal>("Rating").InclusiveBetween(0, 5);
    var rating = Rating.From(-1); //Throws ValidationException
- ExclusiveBetween(T min, T max); //INumber
- PrecisionScale(int precision, int scale); //INumber
- IRuleBuilder<T> IRuleBuilder<T> Matches(string regex); //string
- Must(Expression<Func<T, bool>> predicate); //T

// Complex examples
builder
    .For<int>("UserId")
    .Namespace("MyDomain")
    .AsStruct()
    .Length(20).WithMessage("Please specify a user id with a length of 20.")
    .Matches("[0-9]{5}[a-Z]{15}").WithMessage(x => LocalizedResx.MyMessage)
    .Name("user identifier");

builder
    .For<string>("Title")
    .Normalize(x => x.Replace(" ", "").Trim())            
    .NotEmpty().WithErrorCode("T0001")
    .MaxLength(20).WithMessage(c => $"Please specify a {c.Name} with a max length of {c.MaxLength}.")
    .Must(x => !x.contains("custom"));

// Localization
builder.Localization
    .ForceCultureTo(new CultureInfo("fr-CA"))
    .WithResourceManager<MyLocalizedMessages>();

// Logging
builder.Logging.Fields
    .Clear()
    .AddErrorCode()
    .AddErrorMessage()
    .AddErrorMessageWithPlaceHolders()
    .AddSource()
    .AddPlaceholderValues();

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    // Defer the check to the runtime
    Typely.EnableSensitiveDataLogging(env.IsDevelopment()); //Log AttemptedValue with the error message
}

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

// Type kinds
- AsClass();

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