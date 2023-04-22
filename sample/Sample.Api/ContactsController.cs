using Microsoft.AspNetCore.Mvc;
using Sample.Domain.ContactAggregate;
using Sample.Infrastructure;

namespace Sample.Api;

[Route("api-controller/[controller]/{id}")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ContactsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public string Get(ContactId id) => "lkjlkj" + id;
    
    // [HttpGet]
    // public async Task<ActionResult> Get(ContactId id) =>
    //     await _db.Contacts.FindAsync(id) is Contact contact
    //         ? Ok(contact)
    //         : NotFound();
}