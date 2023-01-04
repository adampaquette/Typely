using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Tests.String;

public class StringConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.String().For("StringType").NotEmpty();
        builder.String().For("NotEmptyType").NotEmpty();
    }
}
