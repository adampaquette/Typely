using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class UnsupportedConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfString().WithName(() => CreateString());
    }
    
    private string CreateString() => "Testing1234".ToUpper();
}