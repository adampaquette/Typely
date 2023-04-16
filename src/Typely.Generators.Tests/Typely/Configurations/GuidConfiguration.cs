using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class GuidConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfGuid().For("Id").WithName(() => LocalizedNames.CustomName).AsClass();
        
        var factory = builder.OfGuid()
            .AsStruct()
            .WithNamespace("Election")
            .NotEmpty().WithMessage("The value cannot be empty").WithErrorCode("ERR-001")
            .AsFactory();
        
        var vote = factory
            .For("Votes")
            .WithName("Presidency vote")
            .NotEqual(Guid.Empty);

        vote.Must(x => x == Guid.Parse("bf820b37-c090-4d51-8a69-07db3d2f42ea"))
            .Must((x) => !x.Equals(10))
            .GreaterThan(Guid.Parse("bf820b37-c090-4d51-8a69-07db3d2f42ea")).WithMessage(() => LocalizedMessages.CustomMessage)
            .GreaterThanOrEqualTo(Guid.Parse("bf820b37-c090-4d51-8a69-07db3d2f42ea")).WithMessage(() => A.CustomLocalization.Value)
            .LessThan(Guid.Parse("bf820b37-c090-4d51-8a69-07db3d2f42ea"))
            .LessThanOrEqualTo(Guid.Parse("bf820b37-c090-4d51-8a69-07db3d2f42ea"));
    }
}