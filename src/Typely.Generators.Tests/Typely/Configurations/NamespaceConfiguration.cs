using Typely.Core;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class NamespaceConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.For<int>("UserId")
            .Namespace("A");

        builder.For<string>("Password")
            .Namespace("B");

        builder.For<Guid>("Key")
            .Namespace("C");
    }
}
