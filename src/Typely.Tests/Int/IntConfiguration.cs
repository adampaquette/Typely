using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Tests.Int;

public class IntConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfInt().For("BasicType");
        builder.OfInt().For("NotEmptyType").NotEmpty();
        builder.OfInt().For("NotEqualType").NotEqual(10);
        builder.OfInt().For("GreaterThanType").GreaterThan(10);
        builder.OfInt().For("GreaterThanOrEqualType").GreaterThanOrEqual(10);
        builder.OfInt().For("LessThanType").LessThan(10);
        builder.OfInt().For("LessThanOrEqualType").LessThanOrEqual(10);
        builder.OfInt().For("MustType").Must((x) => !x.Equals(10));
    }
}