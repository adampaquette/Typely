using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Domain.ContactAggregate;

namespace Sample.Infrastructure;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Phone).IsRequired();
        builder.OwnsMany(x => x.Addresses, a =>
        {
            a.Property(x => x.Id);
            a.Property(x => x.CivicNumber).IsRequired();
            a.Property(x => x.Street).IsRequired();
            a.Property(x => x.City).IsRequired();
            a.Property(x => x.State).IsRequired();
            a.Property(x => x.ZipCode).IsRequired();
        });
    }
}