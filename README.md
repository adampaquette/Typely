<p align="center">
  <img src="https://github.com/adampaquette/Typely/blob/main/assets/logo-300.png" />
</p>

[![build](https://github.com/adampaquette/Typely/actions/workflows/main.yml/badge.svg)](https://github.com/adampaquette/Typely/actions/workflows/main.yml)
[![GitHub last commit](https://img.shields.io/github/last-commit/adampaquette/Typely)](https://github.com/adampaquette/Typely)
[![codecov](https://codecov.io/gh/adampaquette/Typely/branch/main/graph/badge.svg?token=C14WN6VG1H)](https://codecov.io/gh/adampaquette/Typely)

[![Nuget version](https://img.shields.io/nuget/vpre/Typely.Core?label=Typely.Core)](https://www.nuget.org/packages/Typely.Core/)
[![Nuget version](https://img.shields.io/nuget/vpre/Typely.Generators?label=Typely.Generators)](https://www.nuget.org/packages/Typely.Generators/)
[![Nuget version](https://img.shields.io/nuget/vpre/Typely.EfCore?label=Typely.EfCore)](https://www.nuget.org/packages/Typely.EfCore/)
[![Nuget version](https://img.shields.io/nuget/vpre/Typely.AspNetCore.Mvc?label=Typely.AspNetCore.Mvc)](https://www.nuget.org/packages/Typely.AspNetCore.Mvc/)
[![Nuget version](https://img.shields.io/nuget/vpre/Typely.AspNetCore.Swashbuckle?label=Typely.AspNetCore.Swashbuckle)](https://www.nuget.org/packages/Typely.AspNetCore.Swashbuckle/)
[![Nuget version](https://img.shields.io/nuget/vpre/Typely.AspNetCore.Http?label=Typely.AspNetCore.Http)](https://www.nuget.org/packages/Typely.AspNetCore.Http/)


Typely lets you create types easily with a fluent API to embrace Domain-driven design and value objects.

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

Create a class inheriting from `ITypelyConfiguration`
```csharp
public class TypesConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfString().For("FirstName").NotEmpty();    
    }
}
```

Usage
```csharp
var firstName = FirstName.From("Adam");
FirstName.From(""); //Throws ValidationException

if(!FirstName.TryFrom("value", out FirstName instance, out ValidationError? validationError))
{
    // Handle error
}
```