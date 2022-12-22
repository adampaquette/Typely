using FluentType.Core;

namespace Test;

public class TypesConfiguration : IFluentTypesConfiguration
{
    public void Configure(IFluentTypeBuilder builder)
    {
        builder.For<int>("UserId");
    }
}