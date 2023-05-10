using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Specifications;

internal class DateTimeOffsetSpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        var factory = builder.OfDateTimeOffset()
            .AsStruct()
            .WithNamespace("Election")
            .NotEmpty().WithMessage("The value cannot be empty").WithErrorCode("ERR-001")
            .AsFactory();
        
        var vote = factory
            .For("Votes")
            .WithName("Presidency vote")
            .NotEqual(new DateTimeOffset(new DateTime(2022,1,1)));

        vote.Must(x => x == new DateTimeOffset(new DateTime(2022,1,1)))
            .Must((x) => !x.Equals(10))
            .GreaterThan(new DateTimeOffset(new DateTime(2022,1,1))).WithMessage(() => LocalizedMessages.CustomMessage)
            .GreaterThanOrEqualTo(new DateTimeOffset(new DateTime(2022,1,1))).WithMessage(() => A.CustomLocalization.Value)
            .LessThan(new DateTimeOffset(new DateTime(2022,1,1)))
            .LessThanOrEqualTo(new DateTimeOffset(new DateTime(2022,1,1)));
    }
}