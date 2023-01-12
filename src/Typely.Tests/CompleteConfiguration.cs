using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Tests;

public class CompleteConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfString().For("FirstName");

        builder
            .OfString()
            .For("UserId")
            .WithNamespace("UserAggregate")
            .WithName("Owner identifier")
            .AsStruct()
            .NotEmpty().WithMessage("'Name' cannot be empty.").WithErrorCode("ERR001")
            .NotEqual("1");

        builder.OfString().For("EqualityTest");

        builder.OfString().For("ValueType");
        builder.OfString().For("ReferenceType");
    }
}