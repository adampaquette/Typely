![Typely](assets/logo-300.png)

Typely let you create types easily with a fluent API to embrace Domain-driven design and value objects.

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

## Why Typely?

* The entire boiler plate is generated for you. Example [here](https://github.com/adampaquette/Typely/blob/main/src/Typely.Generators.Tests/Typely/Snapshots/TypelyGeneratorSnapshotTests.Complete#UserId.g.verified.cs).
  * Supports value comparison with:
    * `IEquatable<T>`
    * Operators `!=` and `==`
  * Support for sorting with:
    * IComparable\<T>
    * IComparable
  * Support for creating and validating value objects in a generic way.
  * Typely doesn't use the generic `EqualityComparer` and is therefore faster than records.
* Built-in localized validations. You define the rules of the model once.
* Built-in type conversion.
  * Accept value objects directly in the requests of your APIs.
    * No need to manually create theses objects.
    * No need to validate theirs values because they can only be created if they are valid.
  * Save or read your value objects to or from the database without effort.
* Lets you extend your value objects easily:
  * With the use of the interface `ITypelyValue<TValue, TThis>`
  * Or by creating a partial class of the same name and type.
