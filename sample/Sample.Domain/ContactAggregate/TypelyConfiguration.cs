using System.Text.RegularExpressions;
using Typely.Core;
using Typely.Core.Builders;

namespace Sample.Domain.ContactAggregate;

internal class TypelyConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfInt().For("ContactId").GreaterThan(0);
        builder.OfString().For("FirstName").MaxLength(100);
        builder.OfString().For("LastName").MaxLength(100);
        builder.OfString().For("Phone").MaxLength(12).Matches(new Regex("[0-9]{3}-[0-9]{3}-[0-9]{4}"));
        builder.OfString().For("Street").MaxLength(100);
        builder.OfString().For("City").MaxLength(50);
        builder.OfString().For("State").MaxLength(50);
        builder.OfString().For("ZipCode").Matches(new Regex(@"^((\d{5}-\d{4})|(\d{5})|([A-Z|a-z]\d[A-Z|a-z]\d[A-Z|a-z]\d))$"));
    }
}
