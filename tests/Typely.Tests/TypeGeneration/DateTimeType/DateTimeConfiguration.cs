using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Tests.TypeGeneration.DateTimeType;

public class IntConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfDateTime().For("BasicType");
        builder.OfDateTime().For("NotEmptyType").NotEmpty();
        builder.OfDateTime().For("NotEqualType").NotEqual(new DateTime(2023,04,26));
        builder.OfDateTime().For("GreaterThanType").GreaterThan(new DateTime(2023,04,26));
        builder.OfDateTime().For("GreaterThanOrEqualType").GreaterThanOrEqualTo(new DateTime(2023,04,26));
        builder.OfDateTime().For("LessThanType").LessThan(new DateTime(2023,04,26));
        builder.OfDateTime().For("LessThanOrEqualType").LessThanOrEqualTo(new DateTime(2023,04,26));
        builder.OfDateTime().For("MustType").Must((x) => !x.Equals(new DateTime(2023,04,26)));
    }
}