using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Tests;

public class CompleteTypelyConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfInt().For("TypelyOptionTestsType").NotEmpty();
        builder.OfInt().For("ValidationErrorTestsType").NotEmpty();
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