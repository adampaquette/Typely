using System.Text.RegularExpressions;
using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Tests.String;

public class StringConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        // builder.OfString().For("BasicType");
        // builder.OfString().For("NotEmptyType").NotEmpty();
        // builder.OfString().For("NotEqualType").NotEqual("value");
        // builder.OfString().For("GreaterThanType").GreaterThan("value");
        // builder.OfString().For("GreaterThanOrEqualType").GreaterThanOrEqualTo("value");
        // builder.OfString().For("LessThanType").LessThan("value");
        // builder.OfString().For("LessThanOrEqualType").LessThanOrEqualTo("value");
        // builder.OfString().For("MinLengthType").MinLength(5);
        // builder.OfString().For("MaxLengthType").MaxLength(5);
        // builder.OfString().For("LengthType").Length(5, 10);
        // builder.OfString().For("MatchesType").Matches(new Regex("[0-9]{1}"));
        // builder.OfString().For("MustType").Must((x) => !x.Equals("A"));
    }
}