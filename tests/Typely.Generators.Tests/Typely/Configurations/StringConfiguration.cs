using System.Globalization;
using System.Text.RegularExpressions;
using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class StringConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfString().For("Code")
            .AsClass()
            .Length(4)
            .Length(4, 20)
            .NotEqual("0000")
            .Matches(new Regex(".+"))
            .GreaterThan("A")
            .GreaterThanOrEqualTo("A")
            .LessThan("A")
            .LessThanOrEqualTo("A")
            .Normalize(x => x.Trim().ToLower());

        var sf = builder.OfString().AsFactory();
        var sf2 = sf.WithName("Username").AsFactory();
        sf2.For("UserId")
            .WithNamespace("UserAggregate")
            .WithName("Owner identifier")
            .NotEmpty()
            .NotEqual("0").WithMessage("{Name} cannot be equal to {ComparisonValue}.").WithErrorCode("ERR001")
            .MaxLength(20)
            .Must(x => x != "1" && x.ToLower() == "12")
            .Normalize((x) => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(x));
    }
}