using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Tests;

public class CompleteConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.String().For("FirstName");

        builder
            .String()
            .For("UserId")
            .Namespace("UserAggregate")
            .Name("Owner identifier")
            .AsStruct()
            .NotEmpty().WithMessage("'Name' cannot be empty.").WithErrorCode("ERR001")
            .NotEqual("1");

        builder.String().For("EqualityTest");

        builder.String().For("ValueType");
        builder.String().For("ReferenceType");
    }
}
