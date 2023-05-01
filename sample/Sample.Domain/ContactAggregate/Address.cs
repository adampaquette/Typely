namespace Sample.Domain.ContactAggregate;

public class Address
{
    public AddressId Id { get; set; } 
    public CivicNumber CivicNumber { get; set; }
    public Street Street { get; set; }
    public City City { get; set; }
    public State State { get; set; }
    public ZipCode ZipCode { get; set; }
}