namespace Sample.Domain.ContactAggregate;

public class Contact
{
    public ContactId Id { get; set; }
    public required FirstName FirstName { get; set; }
    public required LastName LastName { get; set; }
    public Phone? Phone { get; set; }
    public PhoneType? PhoneType { get; set; }
    
    public required ICollection<Address> Addresses { get; set; }
}
