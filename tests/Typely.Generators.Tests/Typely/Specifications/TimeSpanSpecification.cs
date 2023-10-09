using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Specifications;

internal class TimeSpanSpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        var factory = builder.OfTimeSpan()
            .AsStruct()
            .WithNamespace("Election")
            .NotEmpty().WithMessage("The value cannot be empty").WithErrorCode("ERR-001")
            .AsFactory();

        var vote = factory
            .For("Votes")
            .WithName("Presidency vote")
            .NotEqual(new TimeSpan(2021, 1, 1));

        vote.Must(x => x == new TimeSpan(2022, 1, 1))
            .Must((x) => !x.Equals(10))
            .GreaterThan(new TimeSpan(2022, 1, 1)).WithMessage(() => LocalizedMessages.CustomMessage)
            .GreaterThanOrEqualTo(new TimeSpan(2022, 1, 1)).WithMessage(() => A.CustomLocalization.Value)
            .LessThan(new TimeSpan(2022, 1, 1))
            .LessThanOrEqualTo(new TimeSpan(2022, 1, 1));
    }
}