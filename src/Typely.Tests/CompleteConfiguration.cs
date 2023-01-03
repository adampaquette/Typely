using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Tests;

public class CompleteConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.Of<string>().For("FirstName");

        builder
            .Of<int>()
            .For("UserId")
            .Namespace("UserAggregate")
            .Name("Owner identifier")
            .AsStruct()
            .NotEmpty().WithMessage("'Name' cannot be empty.").WithErrorCode("ERR001")
            .NotEqual(1);

        builder.Of<int>().For("EqualityTest");

        builder.Of<int>().For("ValueType");
        builder.Of<string>().For("ReferenceType");
    }
}
