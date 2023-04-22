using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class DateOnlyConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfDateOnly().For("Id").WithName(() => LocalizedNames.CustomName).AsClass();
        
        var factory = builder.OfDateOnly()
            .AsStruct()
            .WithNamespace("Election")
            .NotEmpty().WithMessage("The value cannot be empty").WithErrorCode("ERR-001")
            .AsFactory();
        
        var vote = factory
            .For("Votes")
            .WithName("Presidency vote")
            .NotEqual(new DateOnly(2021, 1, 1));

        vote.Must(x => x == new DateOnly(2022,1,1))
            .Must((x) => !x.Equals(10))
            .GreaterThan(new DateOnly(2022,1,1)).WithMessage(() => LocalizedMessages.CustomMessage)
            .GreaterThanOrEqualTo(new DateOnly(2022,1,1)).WithMessage(() => A.CustomLocalization.Value)
            .LessThan(new DateOnly(2022,1,1))
            .LessThanOrEqualTo(new DateOnly(2022,1,1));
    }
}