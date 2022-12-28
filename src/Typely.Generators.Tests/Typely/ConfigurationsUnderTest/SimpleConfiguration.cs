using Typely.Core;

namespace Typely.Generators.Tests.Typely.ConfigurationsUnderTest;

public class SimpleConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.For<int>("UserId");
        builder.For<string>("Password");
        builder.For<bool>("IsExcluded");
    }
}
