using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class BoolConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfBool().For("Id").WithName(() => LocalizedNames.CustomName).AsClass();
        
        var factory = builder.OfBool()
            .AsStruct()
            .WithNamespace("Election")
            .NotEmpty().WithMessage("The value cannot be empty").WithErrorCode("ERR-001")
            .AsFactory();
        
        var vote = factory
            .For("Votes")
            .WithName("Presidency vote")
            .NotEqual(false);

        vote.Must(x => x == true).WithMessage(() => LocalizedMessages.CustomMessage)
            .Must((x) => !x.Equals(10)).WithMessage(() => A.CustomLocalization.Value);
    }
}