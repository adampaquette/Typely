using Typely.Core;

namespace Test;

public class TypesConfiguration : ITypelysConfiguration
{
    public void Configure(ITypelysBuilder builder)
    {
        builder.For<int>("UserId");
        builder.For<string>("Username");
    }
}