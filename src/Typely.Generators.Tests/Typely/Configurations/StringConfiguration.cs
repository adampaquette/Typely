using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class StringConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder) 
    {
        var vote = builder.OfInt().For("Votes").NotEmpty().NotEqual(-1);
        vote.For("Vote");

        // builder.OfString().For("Username");
        // builder.OfString().For("Code").Length(4).NotEqual("0000");
        //
        // // Create a reusable factory
        // var sf = builder.OfString().AsFactory();
        //
        // var sf2 = sf.WithName("Username").AsFactory();
        //
        // sf2.For("UserId")
        //     .WithNamespace("UserAggregate")
        //     .WithName("Owner identifier")
        //     .NotEmpty()
        //     .NotEqual("0").WithMessage("{Name} cannot be equal to {ComparisonValue}.").WithErrorCode("ERR001")
        //     .MaxLength(20)
        //     .Must(x => x != "1" && x.ToLower() == "12");
        //
        // // Simplify configurations of similar types.
        // var moment = sf.AsClass()
        //     .WithName(() => LocalizedNames.Moment)
        //     .MinLength(1).WithMessage(() => LocalizedMessages.MinLengthCustom)
        //     .MaxLength(20).WithMessage(() => LocalizedMessages.MaxLengthCustom)
        //     .AsFactory();
        //
        // moment.For("Monday");
        // moment.For("Sunday");
    }
}