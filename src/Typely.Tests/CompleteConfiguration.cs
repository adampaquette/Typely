using Typely.Core;

namespace Typely.Tests;

public class CompleteConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.For<string>("FirstName");

        builder
            .For<int>("UserId")
            .Namespace("UserAggregate")
            .Name("Owner identifier")
            .NotEmpty().WithMessage("'Name' cannot be empty.").WithErrorCode("ERR001")
            .NotEqual(1);

    }
}
