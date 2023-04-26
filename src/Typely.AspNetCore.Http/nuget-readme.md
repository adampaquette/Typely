Typely lets you create types easily with a fluent API to embrace Domain-driven design and value objects.

This library lets you use Typely in ASP.NET Core projects by providing validation exception handling.

# Documentation

- https://docs.typely.net/

# Usage

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Register this middleware before other middleware components
app.UseTypelyValidation();
```
