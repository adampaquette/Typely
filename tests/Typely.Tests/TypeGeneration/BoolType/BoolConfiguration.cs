using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Tests.TypeGeneration.BoolType;

public class BoolConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfBool().For("BasicType");
        builder.OfBool().For("NotEmptyType").NotEmpty();
        builder.OfBool().For("NotEqualType").NotEqual(false);
        builder.OfBool().For("MustType").Must((x) => !x.Equals(10));
    }
}