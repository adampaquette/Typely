using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Specifications;

internal class DateTimeSpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        var factory = builder.OfDateTime()
            .AsStruct()
            .WithNamespace("Election")
            .NotEmpty().WithMessage("The value cannot be empty").WithErrorCode("ERR-001")
            .AsFactory();

        var vote = factory
            .For("Votes")
            .WithName("Presidency vote")
            .NotEqual(new DateTime(2021, 1, 1));

        vote.Must(x => x == new DateTime(2022, 1, 1))
            .Must((x) => !x.Equals(10))
            .GreaterThan(new DateTime(2022, 1, 1)).WithMessage(() => LocalizedMessages.CustomMessage)
            .GreaterThanOrEqualTo(new DateTime(2022, 1, 1)).WithMessage(() => A.CustomLocalization.Value)
            .LessThan(new DateTime(2022, 1, 1))
            .LessThanOrEqualTo(new DateTime(2022, 1, 1));
    }
}