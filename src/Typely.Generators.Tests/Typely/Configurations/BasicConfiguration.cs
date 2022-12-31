using Typely.Core;

namespace Typely.Generators.Tests.Typely.Configurations;

public class BasicConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.For<int>("UserId");
    }
}
