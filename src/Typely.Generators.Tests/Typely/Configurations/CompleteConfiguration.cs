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
            .NotEmpty().WithMessage("'Name' cannot be empty.").WithErrorCode("ERR001")
            .NotEqual(1);        
    }
}
