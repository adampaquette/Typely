using Microsoft.AspNetCore.Mvc;
using Sample.Domain.ContactAggregate;

namespace Sample.Api;

[Route("tests/[controller]")]
[ApiController]
public class TestsController : ControllerBase
{
    [HttpGet("string")]
    public string Foo(FirstName firstName) => $"Value: {firstName}";
    
    [HttpGet("int")]
    public string Foo(ContactId contactId) => $"Value: {contactId}";
}