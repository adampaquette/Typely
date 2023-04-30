using Microsoft.EntityFrameworkCore;
using Sample.Domain.ContactAggregate;
using Sample.Infrastructure;

namespace Sample.Api;

public static class ContactsApi
{
    public static WebApplication RegisterContactsApi(this WebApplication webApplication)
    {
        var contacts = webApplication.MapGroup("/contacts").WithTags("Contacts");

        contacts.MapGet("/", async (AppDbContext db) =>
            await db.Contacts.ToListAsync());

        contacts.MapGet("/{id}", async (ContactId id, AppDbContext db) =>
            await db.Contacts.FindAsync(id)
                is Contact contact
                ? Results.Ok(contact)
                : Results.NotFound());

        contacts.MapPut("/{id}", async (ContactId id, Contact request, AppDbContext db) =>
        {
            var contact = await db.Contacts.FindAsync(id);

            if (contact is null) return Results.NotFound();

            contact.Addresses = request.Addresses;
            contact.FirstName = request.FirstName;
            contact.LastName = request.LastName;
            contact.Phone = request.Phone;
            contact.PhoneType = request.PhoneType;

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        contacts.MapPost("/", async (Contact contact, AppDbContext db) =>
        {
            db.Contacts.Add(contact);
            await db.SaveChangesAsync();

            return Results.Created($"/contacts/{contact.Id}", contact);
        });

        contacts.MapDelete("/{id}", async (ContactId id, AppDbContext db) =>
        {
            if (await db.Contacts.FindAsync(id) is Contact contact)
            {
                db.Contacts.Remove(contact);
                await db.SaveChangesAsync();
                return Results.Ok(contact);
            }

            return Results.NotFound();
        });

        return webApplication;
    }
}