namespace Typely.EfCore.Tests.Common;

public class Person
{
    public PersonId Id { get; set; }
    public required FirstName FirstName { get; set; }
    public required Email Email { get; set; }
}