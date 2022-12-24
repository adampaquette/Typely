using FluentType.Core;

namespace Test;

public class TypesConfiguration : IFluentTypesConfiguration
{
    public void Configure(IFluentTypesBuilder builder)
    {
        builder.For<int>("UserId");
        builder.For<string>("Username");

    }
}