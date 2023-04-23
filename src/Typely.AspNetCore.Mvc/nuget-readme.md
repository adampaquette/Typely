Typely lets you create types easily with a fluent API to embrace Domain-driven design and value objects.

This library lets you use Typely in ASP.NET Core MVC projects by providing type binding.

# Documentation

- https://docs.typely.net/

# Usage

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options => options.UseTypelyModelBinderProvider());
```
