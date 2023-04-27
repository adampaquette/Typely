using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Tests.TypeGeneration.Char;

public class IntConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfChar().For("BasicType");
        builder.OfChar().For("NotEmptyType").NotEmpty();
        builder.OfChar().For("NotEqualType").NotEqual('A');
        builder.OfChar().For("GreaterThanType").GreaterThan('B');
        builder.OfChar().For("GreaterThanOrEqualType").GreaterThanOrEqualTo('B');
        builder.OfChar().For("LessThanType").LessThan('A');
        builder.OfChar().For("LessThanOrEqualType").LessThanOrEqualTo('A');
        builder.OfChar().For("MustType").Must((x) => !x.Equals('A'));
    }
}