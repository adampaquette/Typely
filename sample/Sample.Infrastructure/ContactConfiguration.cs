using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Domain.ContactAggregate;
using Typely.EfCore;

namespace Sample.Infrastructure;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion<TypelyValueConverter<int, ContactId>>();
        builder.Property(x => x.FirstName)
            .HasConversion<TypelyValueConverter<string, FirstName>>()
            .IsRequired();
        builder.Property(x => x.LastName)
            .HasConversion<TypelyValueConverter<string, LastName>>()
            .IsRequired();
        builder.Property(x => x.Phone)
            .HasConversion<TypelyValueConverter<string, Phone>>()
            .IsRequired();
        builder.Property(x => x.Street)
            .HasConversion<TypelyValueConverter<string, Street>>()
            .IsRequired();
        builder.Property(x => x.City)
            .HasConversion<TypelyValueConverter<string, City>>()
            .IsRequired();
        builder.Property(x => x.State)
            .HasConversion<TypelyValueConverter<string, State>>()
            .IsRequired();
        builder.Property(x => x.ZipCode)
            .HasConversion<TypelyValueConverter<string, ZipCode>>()
            .IsRequired();
    }
}