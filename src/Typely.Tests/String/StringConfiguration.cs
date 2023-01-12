using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Tests.String;

public class StringConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfString().For("StringType").NotEmpty();
        builder.OfString().For("NotEmptyType").NotEmpty();
    }
}