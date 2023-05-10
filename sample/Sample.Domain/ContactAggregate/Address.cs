namespace Sample.Domain.ContactAggregate;

public class Address
{
    public AddressId Id { get; set; }
    public required CivicNumber CivicNumber { get; set; }
    public required Street Street { get; set; }
    public required City City { get; set; }
    public required State State { get; set; }
    public required ZipCode ZipCode { get; set; }
}