using Microsoft.EntityFrameworkCore;
using Sample.Api;
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
app.RegisterContactsApi();

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
            Addresses = new List<Address>
            {
                new()
                {
                    Street = Street.From("123 Main St."),
                    City = City.From("Anytown"),
                    State = State.From("CA"),
                    ZipCode = ZipCode.From("A1P2W3")
                }
            }
        });
        db.SaveChanges();
    }
}