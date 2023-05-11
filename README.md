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

Unleashing the power of value object creation with a fluent Api.

## Example

```csharp
public class TypesSpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        builder.OfInt().For("ContactId").GreaterThan(0);
        
        builder.OfString()
            .For("InsuranceCode")
            .NotEmpty()
            .Length(10)
            .Must(x => x.StartsWith("A91"))
            .Normalize(x => x.ToUpper());

        builder.OfString()
            .For("Phone")
            .MaxLength(12)
            .Matches(new Regex("[0-9]{3}-[0-9]{3}-[0-9]{4}"));
        
        var title = builder.OfString()
            .NotEmpty()
            .MaxLength(100)
            .Normalize(x => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(x));

        title.For("FirstName");
        title.For("LastName");

        builder.OfDecimal()
            .For("CouponDiscount")
            .WithName("Coupon discount")
            .NotEmpty().WithMessage("{Name} cannot be empty").WithErrorCode("ERR-001")
            .GreaterThan(0).WithMessage(() => LocalizedMessages.CustomMessage)
            .LessThanOrEqualTo(24.99M);
    }
}
```

# Documentation

- https://docs.typely.net/

# Prerequisites

- Supported .NET versions
    - .NET 7.0 and greater

# Getting started

1. Install packages
    ```
    dotnet add package Typely.Core
    dotnet add package Typely.Generators
    ```

2. Create a class inheriting from `ITypelySpecification`
    ```csharp
    public class TypesSpecification : ITypelySpecification
    {
        public void Create(ITypelyBuilder builder)
        {
            builder.OfString().For("FirstName").NotEmpty();    
        }
    }
    ```

3. Usage
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

[![Nuget version](https://img.shields.io/nuget/vpre/Typely.AspNetCore?label=Typely.AspNetCore)](https://www.nuget.org/packages/Typely.AspNetCore/)

To support validation handling and MVC model binding, include `Typely.AspNetCore` in your projects.

## Nuget package
```
dotnet add package Typely.AspNetCore
```

## Model binding

- By default Minimal Apis are supported by implementing a `TryParse` function for generated types.
- Post requests are supported by `TypelyJsonConverter`.
- Other bindings are supported by `TypelyTypeConverter`.

These are included by default with `Typely.Core`.

## Model state

If you want to add validation errors into the model state of MVC during the binding phase of the request, configure the option below: 
### Configuration
```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options => options.UseTypelyModelBinderProvider());
```

It supports many validation errors without using exceptions.

## Validation errors

The following middleware allows you to return neatly structured Json error responses compatible with `Microsoft.AspNetCore.Http.HttpValidationProblemDetails`.

### Configuration
```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Register this middleware before other middleware components
app.UseTypelyValidationResult();
```

It works by catching a `ValidationException` thrown by Typely and returns a list of errors associated to their value object type name as well as the templates used, if you want to modfify the messages in your client application.

### Example of response:
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

# OpenAPI

[![Nuget version](https://img.shields.io/nuget/vpre/Typely.AspNetCore.Swashbuckle?label=Typely.AspNetCore.Swashbuckle)](https://www.nuget.org/packages/Typely.AspNetCore.Swashbuckle/)

To add support for OpenAPI specs and Swagger UI, include `Typely.AspNetCore.Swashbuckle` in your projects.
## Nuget package
```
dotnet add package Typely.AspNetCore.Swashbuckle
```
## Configuration
```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen(options => options.UseTypelySchemaFilter());
```

# Entity Framework Core

[![Nuget version](https://img.shields.io/nuget/vpre/Typely.EfCore?label=Typely.EfCore)](https://www.nuget.org/packages/Typely.EfCore/)

To use your value objects with EF Core, include `Typely.EfCore` in your projects.

## Nuget package
```
dotnet add package Typely.EfCore
```
## Configuration
Apply Typely conventions to your `DbContext` to automatically configure the database. By default, the conventions will tell EF Core how to save and load your value objects and set the maximum data length. 
```csharp
protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
{
    configurationBuilder.Conventions.AddTypelyConventions();
    //OR
    configurationBuilder.Conventions
        .AddTypelyConversionConvention()
        .AddTypelyMaxLengthConvention();
}
```
If you don't use conventions, you can configure your types manually. Typely will generate a `MaxLength` property when using a validation that sets the maximum length.
```csharp
builder.Property(x => x.LastName)
    .HasMaxLength(LastName.MaxLength)
    .HasConversion<TypelyValueConverter<string, LastName>>();
```
You can also override the default conventions:
```csharp
builder.Property(x => x.FirstName)
    .HasMaxLength(80)
    .HasConversion((x) => x + "-custom-conversion", (x) => FirstName.Create(x.Replace("-custom-conversion", "")));
```