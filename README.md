<p align="center">
  <img src="https://github.com/adampaquette/Typely/blob/main/assets/logo-300.png" />
</p>

[![Nuget version](https://img.shields.io/nuget/v/Typely?label=nuget%20version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/Typely/)
[![GitHub last commit](https://img.shields.io/github/last-commit/adampaquette/Typely?logo=github)](https://github.com/adampaquette/Typely)

Typerly lets you create types easily with a fluent API to embrace Domain-driven design and value objects.

## Example

```csharp
public class TypesConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfInt().For("Votes");        
        builder.OfString().For("Code").Length(4).NotEqual("0000");
        
        builder.OfString()
            .For("UserId")
            .WithNamespace("UserAggregate")
            .WithName("Owner identifier")
            .NotEmpty()
            .NotEqual("0").WithMessage("{Name} cannot be equal to {ComparisonValue}.").WithErrorCode("ERR001")
            .MaxLength(20);

        builder.OfString()
            .For("Monday")
            .AsClass()
            .WithName(() => LocalizedNames.Moment)
            .MinLength(1).WithMessage(() => LocalizedMessages.MinLengthCustom)
            .MaxLength(20).WithMessage(() => LocalizedMessages.MaxLengthCustom);
    }
}
```

# Documentation

- https://docs.typely.net/

# Prerequisites

- Supported .NET versions
    - .NET 7.0 and greater

# Getting started

Install packages
```
dotnet add package Typely.Core
dotnet add package Typely.Generators
```

Create a class inheriting from `ÌTypelyConfiguration`
```
public class TypesConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfInt().For("FirstName").NotEmpty();    
    }
}
```

Usage
```
var firstName = FirstName.From("Adam");
FirstName.From(""); //Throws ValidationException

if(!FirstName.TryFrom("value", out FirstName instance, out ValidationError? validationError))
{
    // Handle error
}
```