<p align="center">
  <img src="https://github.com/adampaquette/Typely/blob/main/assets/logo-300.png" />
</p>

[![build](https://github.com/adampaquette/Typely/actions/workflows/main.yml/badge.svg)](https://github.com/adampaquette/Typely/actions/workflows/main.yml)
[![GitHub last commit](https://img.shields.io/github/last-commit/adampaquette/Typely)](https://github.com/adampaquette/Typely)
[![codecov](https://codecov.io/gh/adampaquette/Typely/branch/main/graph/badge.svg?token=C14WN6VG1H)](https://codecov.io/gh/adampaquette/Typely)

[![Nuget version](https://img.shields.io/nuget/vpre/Typely.Core?label=Typely.Core)](https://www.nuget.org/packages/Typely.Core/)
[![Nuget version](https://img.shields.io/nuget/vpre/Typely.Generators?label=Typely.Generators)](https://www.nuget.org/packages/Typely.Generators/)
[![Nuget version](https://img.shields.io/nuget/vpre/Typely.EfCore?label=Typely.EfCore)](https://www.nuget.org/packages/Typely.EfCore/)
[![Nuget version](https://img.shields.io/nuget/vpre/Typely.AspNetCore?label=Typely.AspNetCore)](https://www.nuget.org/packages/Typely.AspNetCore/)
[![Nuget version](https://img.shields.io/nuget/vpre/Typely.AspNetCore.Swashbuckle?label=Typely.AspNetCore.Swashbuckle)](https://www.nuget.org/packages/Typely.AspNetCore.Swashbuckle/)

Typely: Unleashing the power of value object creation with a fluent Api.

## Example

```csharp
public class TypesSpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        builder.OfInt().For("ContactId").GreaterThan(0);

        builder.OfString()
            .For("Phone")
            .MaxLength(12)
            .Matches(new Regex("[0-9]{3}-[0-9]{3}-[0-9]{4}"));
        
        builder.OfString()
            .For("ZipCode")
            .Matches(new Regex(@"^((\d{5}-\d{4})|(\d{5})|([A-Z|a-z]\d[A-Z|a-z]\d[A-Z|a-z]\d))$"))
            .Normalize(x => x.ToUpper());

        var title = builder.OfString()
            .NotEmpty()
            .MaxLength(100)
            .Normalize(x => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(x));

        title.For("FirstName");
        title.For("LastName");
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

Create a class inheriting from `ITypelySpecification`
```csharp
public class TypesSpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
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

# Json Serialization

Serialization using System.Text.Json is supported by default and will only write the underlying value.

# ASP.NET Core

To use Typely in ASP.NET Core projects with support for validation handling and MVC model binding, add the following nuget package:
```
dotnet add package Typely.AspNetCore
```

## Model binding

- By default Minimal Apis are supported by implementing a `TryParse` function for generated types.
- Post requests are supported by the `TypelyJsonConverter`.
- Other bindings are supported by `TypelyTypeConverter`.

## Validation errors

You can return well formatted Json error responses with the following middleware:
```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Register this middleware before other middleware components
app.UseTypelyValidationResult();
```

It catches a `ValidationException` thrown by Typely and return a compatible `Microsoft.AspNetCore.Http.HttpValidationProblemDetails` class serialized.

Example of response:
```json
{
  "templates": {
    "ZipCode": [
      {
        "code": "Matches",
        "message": "'{Name}' is not in the correct format. Expected format '{RegularExpression}'.",
        "typeName": "ZipCode",
        "placeholderValues": {
          "RegularExpression": "^((\\d{5}-\\d{4})|(\\d{5})|([A-Z|a-z]\\d[A-Z|a-z]\\d[A-Z|a-z]\\d))$",
          "Name": "ZipCode",
          "ActualLength": "7"
        }
      }
    ]
  },
  "errors": {
    "ZipCode": [
      "'ZipCode' is not in the correct format. Expected format '^((\\d{5}-\\d{4})|(\\d{5})|([A-Z|a-z]\\d[A-Z|a-z]\\d[A-Z|a-z]\\d))$'."
    ]
  },
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400
}
```

## Model state

If you want to add validation errors into the model state of MVC during the binding phase of the request, configure the below option: 
```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options => options.UseTypelyModelBinderProvider());
```

It supports many validation errors without using exceptions.