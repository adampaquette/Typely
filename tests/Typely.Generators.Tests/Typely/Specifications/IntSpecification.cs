﻿using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Specifications;

internal class IntSpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        var factory = builder.OfInt()
            .AsStruct()
            .WithNamespace("Election")
            .NotEmpty().WithMessage("The value cannot be empty").WithErrorCode("ERR-001")
            .AsFactory();

        var vote = factory
            .For("Votes")
            .WithName("Presidency vote")
            .Normalize(abc => abc + 1)
            .NotEqual(-1);

        vote.Must(x => x == 122)
            .Must((x) => !x.Equals(10))
            .GreaterThan(10).WithMessage(() => LocalizedMessages.CustomMessage)
            .GreaterThanOrEqualTo(10).WithMessage(() => A.CustomLocalization.Value)
            .LessThan(20)
            .LessThanOrEqualTo(20);
    }
}