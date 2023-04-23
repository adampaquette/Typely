using Microsoft.AspNetCore.Mvc;
using Sample.Domain.ContactAggregate;
using Sample.Infrastructure;
using System.ComponentModel;
using System.Globalization;

namespace Sample.Api;

[Route("api-controller/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ContactsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("Typely-string")]
    public string Foo(FirstName myType) => $"Hallo {myType}";
    [HttpGet("Typely-int")]
    public string Foo(ContactId myType) => $"Hallo {myType}";

    // [HttpGet]
    // public async Task<ActionResult> Get(ContactId id) =>
    //     await _db.Contacts.FindAsync(id) is Contact contact
    //         ? Ok(contact)
    //         : NotFound();
}

public class MyType
{
    public string? Value { get; set; }
}

[TypeConverter(typeof(MyTypeConverter))]
public class MyTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string);
    }

    public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        return new MyType { Value = value.ToString() };
    }
}