Typely: Unleashing the power of value object creation with a fluent Api.

## Example

```csharp
public class TypesSpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
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