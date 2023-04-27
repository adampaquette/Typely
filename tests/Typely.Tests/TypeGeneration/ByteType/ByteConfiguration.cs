using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Tests.TypeGeneration.ByteType;

public class ByteConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfByte().For("BasicType");
        builder.OfByte().For("NotEmptyType").NotEmpty();
        builder.OfByte().For("NotEqualType").NotEqual(10);
        builder.OfByte().For("GreaterThanType").GreaterThan(10);
        builder.OfByte().For("GreaterThanOrEqualType").GreaterThanOrEqualTo(10);
        builder.OfByte().For("LessThanType").LessThan(10);
        builder.OfByte().For("LessThanOrEqualType").LessThanOrEqualTo(10);
        builder.OfByte().For("MustType").Must((x) => !x.Equals(10));
    }
}