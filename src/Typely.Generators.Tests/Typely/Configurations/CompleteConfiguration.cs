using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class CompleteConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder) 
    {
        builder.OfInt().For("Votes"); 
        //builder.OfString().For("Username");
        //builder.OfString().For("Code").Length(4).NotEqual("0000");

        //// Create a reusable factory
        var sf = builder.OfString().AsFactory();    

        //sf.For("UserId")
        //    .WithNamespace("UserAggregate")
        //    .WithName("Owner identifier")
        //    .NotEmpty()
        //    .NotEqual("0").WithMessage("{Name} cannot be equal to {ComparisonValue}.").WithErrorCode("ERR001")
        //    .MaxLength(20);

        //// Simplify configurations of similar types.
        //var moment = sf.AsClass()
        //    .WithName(() => LocalizedNames.Moment)
        //    .MinLength(1).WithMessage(() => LocalizedMessages.MinLengthCustom)
        //    .MaxLength(20).WithMessage(() => LocalizedMessages.MaxLengthCustom)
        //    .AsFactory();

        //moment.For("Monday");
        //moment.For("Sunday");
    }
}