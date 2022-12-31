using Typely.Core;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class NotEmptyConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.For<int>("UserId")
            .NotEmpty();

        builder.For<string>("Name")
            .NotEmpty();
    }
}
