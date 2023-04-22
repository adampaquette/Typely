namespace Sample.Domain.ContactAggregate;

public class Contact
{
    public ContactId Id { get; set; }
    public FirstName? FirstName { get; set; }
    public LastName? LastName { get; set; }
    public Phone? Phone { get; set; }
    public Street? Street { get; set; }
    public City? City { get; set; }
    public State? State { get; set; }
    public ZipCode? ZipCode { get; set; }
}
