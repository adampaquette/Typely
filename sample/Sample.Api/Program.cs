using Microsoft.EntityFrameworkCore;
using Sample.Domain.ContactAggregate;
using Sample.Infrastructure;
using Typely.AspNetCore.Http;
using Typely.AspNetCore.Mvc;
using Typely.AspNetCore.Swashbuckle;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data Source=sample.db"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(options => options.UseTypelyModelBinderProvider());
builder.Services.AddSwaggerGen(options => options.UseTypelySchemaFilter());

var app = builder.Build();

// Register this middleware before other middleware components
app.UseTypelyValidation();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

var contacts = app.MapGroup("/contacts").WithTags("Contacts");

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

    contact.City = request.City;
    contact.FirstName = request.FirstName;
    contact.LastName = request.LastName;
    contact.Phone = request.Phone;
    contact.State = request.State;
    contact.Street = request.Street;
    contact.ZipCode = request.ZipCode;

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

InitDatabase();

app.Run();

void InitDatabase()
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.Migrate();

    if (!db.Contacts.Any())
    {
        db.Contacts.Add(new Contact
        {
            Id = ContactId.From(1),
            FirstName = FirstName.From("John"),
            LastName = LastName.From("Doe"),
            Phone = Phone.From("555-555-5555"),
            Street = Street.From("123 Main St."),
            City = City.From("Anytown"),
            State = State.From("CA"),
            ZipCode = ZipCode.From("A1P2W3")
        });
        db.SaveChanges();
    }
}