using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Domain.ContactAggregate;

namespace Sample.Infrastructure;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        ConfigureContactProperties(builder);
        ConfigureAddressProperties(builder);
        SeedInitialData(builder);
    }

    private static void ConfigureContactProperties(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Phone).IsRequired();
    }

    private static void ConfigureAddressProperties(EntityTypeBuilder<Contact> builder)
    {
        builder.OwnsMany(x => x.Addresses, a =>
        {
            a.Property(x => x.Id);
            a.Property(x => x.CivicNumber).IsRequired();
            a.Property(x => x.Street).IsRequired();
            a.Property(x => x.City).IsRequired();
            a.Property(x => x.State).IsRequired();
            a.Property(x => x.ZipCode).IsRequired();
            a.WithOwner().HasForeignKey("ContactId");
        });
    }

    private static void SeedInitialData(EntityTypeBuilder<Contact> builder)
    {
        var contacts = GetInitialContacts();

        SeedContacts(builder, contacts);
        SeedAddresses(builder, contacts);
    }

    private static List<Contact> GetInitialContacts()
    {
        return new List<Contact>
        {
            new()
            {
                Id = new ContactId(1),
                FirstName = new FirstName("John"),
                LastName = new LastName("Doe"),
                Phone = new Phone("123-456-7890"),
                PhoneType = PhoneType.Mobile,
                Addresses = new List<Address>
                {
                    new()
                    {
                        Id = new AddressId(2),
                        CivicNumber = new CivicNumber(456),
                        Street = new Street("Oak Ave"),
                        City = new City("Other City"),
                        State = new State("NY"),
                        ZipCode = new ZipCode("67890")
                    }
                }
            },
            new()
            {
                Id = new ContactId(2),
                FirstName = new FirstName("Jane"),
                LastName = new LastName("Smith"),
                Phone = new Phone("987-654-3210"),
                PhoneType = PhoneType.Home,
                Addresses = new List<Address>
                {
                    new()
                    {
                        Id = new AddressId(1),
                        CivicNumber = new CivicNumber(123),
                        Street = new Street("Main St"),
                        City = new City("Anytown"),
                        State = new State("CA"),
                        ZipCode = new ZipCode("12345")
                    }
                }
            }
        };
    }

    private static void SeedContacts(EntityTypeBuilder<Contact> builder, List<Contact> contacts)
    {
        builder.HasData(contacts.Select(c => new Contact
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Phone = c.Phone,
            PhoneType = c.PhoneType,
            Addresses = new List<Address>()
        }));
    }

    private static void SeedAddresses(EntityTypeBuilder<Contact> builder, List<Contact> contacts)
    {
        builder.OwnsMany(c => c.Addresses).HasData(
            contacts.SelectMany(c => c.Addresses.Select(a => new
            {
                ContactId = c.Id,
                a.Id,
                a.CivicNumber,
                a.Street,
                a.City,
                a.State,
                a.ZipCode
            }))
        );
    }
}