using Typely.Core;

namespace Typely.Tests;

public class BasicConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.For<string>("FirstName");
    }
}
