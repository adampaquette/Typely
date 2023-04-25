namespace Typely.EfCore.Tests.Common;

public class Person
{
    public PersonId Id { get; set; }
    public required FirstName FirstName { get; set; }
    public Email? Email { get; set; }
}