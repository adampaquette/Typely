using Typely.Core;

namespace Typely.Generators.Tests.Typely.ConfigurationsUnderTest;

internal class NamespaceConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.For<int>("UserId")
            .Namespace("A");

        builder.For<string>("Password")
            .Namespace("B");

        builder.For<string>("Password2")
            .Namespace("C");
    }
}
