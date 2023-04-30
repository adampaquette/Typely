Typely lets you create types easily with a fluent API to embrace Domain-driven design and value objects.

This library lets you use Typely in ASP.NET Core projects by providing validation exception handling and model binding.

# Documentation

- https://docs.typely.net/

# Usage

This middleware will catch any `ValidationException` thrown by Typely and return a JSON response with the validation errors.
```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Register this middleware before other middleware components
app.UseTypelyValidationResult();
```
Example of response:
````
{
  "errorTemplates": {
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
````

If you need to add Typely's validation errors into the model state of MVC, use the model binder below: 
```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options => options.UseTypelyModelBinderProvider());
```