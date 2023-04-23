Typely lets you create types easily with a fluent API to embrace Domain-driven design and value objects.

This library lets you use Typely with Entity Framework Core by providing a set of conventions.

# Documentation

- https://docs.typely.net/

# Usage

```csharp
protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
{
    configurationBuilder.Conventions.AddTypelyConventions();
}
```
