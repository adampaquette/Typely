using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Configurations.A;

internal class MultipleConfigurationA : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfInt().For("Votes");
        //builder.OfString().For("Name");
    }
}