using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class CompleteConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfString().For("Username");
        builder.OfString().For("Code").Length(4).NotEqual("0000");

        // Create a factory of string type.
        var sf = builder.OfString().AsFactory();

        sf.For("UserId")
            .WithNamespace("UserAggregate")
            .WithName("Owner identifier")
            .NotEmpty()
            .NotEqual("0").WithMessage("{Name} cannot be equal to {ComparisonValue}.").WithErrorCode("ERR001")
            .MaxLength(20);

        // Insure the integrity of similar type's definition with a factory or simplify configurations.
        var moment = sf.AsClass()
            .WithName(() => LocalizedNames.Moment)
            .MinLength(1).WithMessage(() => LocalizedMessages.MinLengthCustom)
            .MaxLength(20).WithMessage(() => LocalizedMessages.MaxLengthCustom)
            .AsFactory();

        // Generate similar types
        moment.For("Monday");
        moment.For("Sunday");
    }
}