using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class IntConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfInt().For("Id");
        
        var factory = builder.OfInt()
            .WithNamespace("Election")
            .NotEmpty().WithMessage("The value cannot be empty").WithErrorCode("ERR-001")
            .AsFactory();
        
        var vote = factory
            .For("Votes")
            .WithName("Presidency vote")
            .NotEqual(-1);

        vote.Must(x => x == 122)
            .GreaterThan(10)
            .GreaterThanOrEqualTo(10)
            .LessThan(20)
            .LessThanOrEqualTo(20);
    }
}