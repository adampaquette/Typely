using FluentType.Core;

namespace FluentType.Playground;

public class TypesConfiguration : IFluentTypesConfiguration
{
    public void Configure(IFluentTypeBuilder builder)
    {
        builder.For<int>("Likes");
        //builder.For<decimal>("Rating").InclusiveBetween(0, 5);
        //builder.For<string>("FirstName").NotEmpty();

        //builder
        //    .For<int>("UserId")
        //    .Namespace("MyDomain")
        //    .AsStruct()
        //    .Length(20)
        //    .WithMessage("Pleasy specify a {Name} with a length of {Length}.")
        //    .Matches("[0-9]{5}[a-Z]{15}")
        //    .WithName("user identifier");
    }
}
