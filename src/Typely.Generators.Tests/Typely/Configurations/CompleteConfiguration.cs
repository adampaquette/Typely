using Typely.Core;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class CompleteConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder
            .For<int>("UserId")
            .Namespace("UserAggregate")
            .Name("Owner identifier")
            .AsStruct()
            .NotEmpty()
            .NotEqual(100).WithMessage("{Name} cannot be equal to {ComparisonValue}.").WithErrorCode("ERR001");

        builder.For<int>("UPC")
            .Name(() => Names.UniversalProductCode)
            .NotEmpty().WithMessage(() => ErrorMessages.NotEqual);
    }
}
