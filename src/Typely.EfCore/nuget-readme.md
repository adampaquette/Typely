Typely: Unleashing the power of value object creation with a fluent Api.

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
