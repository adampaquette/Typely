using System.Globalization;
using System.Text.RegularExpressions;
using Typely.Core;
using Typely.Core.Builders;

namespace Sample.Domain.ContactAggregate;

internal class TypelySpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        const string myVar1 = "MyVar1";
        string myVar2 = "MyVar2";
        
        builder.OfString().For(myVar1);
        builder.OfString().For(myVar2);
        
        //Contact
        builder.OfInt().For("ContactId").GreaterThan(0);
        
        builder.OfString()
            .For("Phone")
            .MaxLength(12)
            .Matches(new Regex("[0-9]{3}-[0-9]{3}-[0-9]{4}"));
        
        builder.OfString()
            .For("ZipCode")
            .Matches(new Regex(@"^((\d{5}-\d{4})|(\d{5})|([A-Z|a-z]\d[A-Z|a-z]\d[A-Z|a-z]\d))$"))
            .Normalize(x => x.ToUpper());

        var title = builder.OfString()
            .NotEmpty()
            .MaxLength(100)
            .Normalize(x => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(x));

        title.For("FirstName");
        title.For("LastName");
        title.For("Street");
        title.For("City");
        title.For("State");

        //Address
        builder.OfInt().For("AddressId").GreaterThan(0);
        builder.OfInt().For("CivicNumber").GreaterThan(0);
    }
}