using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class CompleteConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfString().For("Username");

        builder.OfString()
            .For("UserId")
            .WithNamespace("UserAggregate")
            .WithName("Owner identifier")
            .AsStruct()
            .NotEmpty()
            .NotEqual("100").WithMessage("{Name} cannot be equal to {ComparisonValue}.").WithErrorCode("ERR001")
            .MaxLength(100);

        // Factory of string type
        var str = builder.OfString().AsFactory();

        // Combine factories
        var moment = str.AsClass()
            .MinLength(1)
            .MaxLength(20)
            .AsFactory();

        // Generate similar types
        moment.For("Monday");
        moment.For("Sunday");
    }
}
