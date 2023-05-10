Typely: Unleashing the power of value object creation with a fluent Api.

This library lets you use Typely in ASP.NET Core by generating OpenAPI documentation with Swashbuckle.

# Documentation

- https://docs.typely.net/

# Usage
```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen(options => options.UseTypelySchemaFilter());
```