using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Sample.Domain.ContactAggregate;
using Typely.Core;

namespace Sample.Api;

[Route("tests/[controller]")]
[ApiController]
public class TestsController : ControllerBase
{
    [HttpGet("string")]
    public string Foo(FirstName firstName) => $"Value: {firstName}";
    
    [HttpGet("int")]
    public string Foo(ContactId contactId) => $"Value: {contactId}";

    [HttpPost("validation-problem")]
    public IActionResult ValidationProblem(Contact contact)
    {
        var error = new ValidationError("A", "AAA", null, "type", new Dictionary<string, object?>());
        var exp = new ValidationException(error);

        ModelState.AddModelError("FirstName", "Validation error message");
        return ValidationProblem();
    }
}

public class Myaa : ModelError
{
    public Myaa(Exception exception) : base(exception)
    {
    }

    public Myaa(Exception exception, string? errorMessage) : base(exception, errorMessage)
    {
    }

    public Myaa(string? errorMessage) : base(errorMessage)
    {
    }
}