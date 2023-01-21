using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Tests;

public class CompleteConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfString().For("SerializationTestsType");    
        builder.OfString().For("TypelyOptionTestsType").NotEmpty();
        builder.OfString().For("ValidationErrorTestsType").NotEmpty();
        var factory = builder.OfString().AsFactory();
        factory.For("BasicType");
        factory.For("BasicType2");

        builder
            .OfString()
            .For("UserId")
            .WithNamespace("UserAggregate")
            .WithName("Owner identifier")
            .AsStruct()
            .NotEmpty().WithMessage("'Name' cannot be empty.").WithErrorCode("ERR001")
            .NotEqual("1"); 
    }
}