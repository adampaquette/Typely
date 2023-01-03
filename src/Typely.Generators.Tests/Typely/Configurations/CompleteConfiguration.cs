using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class CompleteConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder
            .String()
            .For("UserId")
            .Namespace("UserAggregate")
            .Name("Owner identifier")
            .AsStruct()
            .NotEmpty()
            .NotEqual("100").WithMessage("{Name} cannot be equal to {ComparisonValue}.").WithErrorCode("ERR001");

        builder
            .String()
            .For("UPC")
            //.Name(() => Names.UniversalProductCode) 
            .NotEmpty().WithMessage(() => ErrorMessages.NotEqual);

        builder.String().For("Title").NotEmpty().MaxLength(100);
    }
}
