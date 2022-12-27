using Typely.Core;

namespace Test;

public class TypesConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.For<int>("UserId");
        builder.For<string>("Username");
    }
}