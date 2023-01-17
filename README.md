<p align="center">
  <img src="https://github.com/adampaquette/Typely/blob/main/assets/logo-300.png" />
</p>

![Typely](https://github.com/adampaquette/Typely/blob/main/assets/logo-300.png)

Typerly lets you create types easily with a fluent API to embrace Domain-driven design and value objects.

## Example

```csharp
public class TypesConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfInt().For("Votes");
        builder.OfString().For("Username");
        builder.OfString().For("Code").Length(4).NotEqual("0000");

        // Create a reusable factory
        var sf = builder.OfString().AsFactory();

        sf.For("UserId")
            .WithNamespace("UserAggregate")
            .WithName("Owner identifier")
            .NotEmpty()
            .NotEqual("0").WithMessage("{Name} cannot be equal to {ComparisonValue}.").WithErrorCode("ERR001")
            .MaxLength(20);

        // Simplify configurations of similar types.
        var moment = sf.AsClass()
            .WithName(() => LocalizedNames.Moment)
            .MinLength(1).WithMessage(() => LocalizedMessages.MinLengthCustom)
            .MaxLength(20).WithMessage(() => LocalizedMessages.MaxLengthCustom)
            .AsFactory();

        moment.For("Monday");
        moment.For("Sunday");
    }
}

// Examples of uses:
var code = Code.From("!c9u");
var userId = UserId.From(""); //Throws ValidationException

if(!UserId.TryFrom("value", out Rating instance, out ValidationError? validationError))
{
    // Handle error
}
```

# Read the docs

https://docs.typely.net/