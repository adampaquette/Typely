using Microsoft.AspNetCore.Mvc;
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
        var error = new ValidationError("A", "AAA", null, "type", new Dictionary<string, string?>());
        var exp = new ValidationException(error);

        ModelState.AddModelError("FirstName", "Validation error message");
        return ValidationProblem();
    }
}