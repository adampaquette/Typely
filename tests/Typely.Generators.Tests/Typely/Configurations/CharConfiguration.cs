﻿using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class CharConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfChar().For("Id").WithName(() => LocalizedNames.CustomName).AsClass();
        
        var factory = builder.OfChar()
            .AsStruct()
            .WithNamespace("Election")
            .NotEmpty().WithMessage("The value cannot be empty").WithErrorCode("ERR-001")
            .AsFactory();
        
        var vote = factory
            .For("Votes")
            .WithName("Presidency vote")
            .NotEqual('a');

        vote.Must(x => x == 122)
            .Must((x) => !x.Equals(10))
            .GreaterThan('a').WithMessage(() => LocalizedMessages.CustomMessage)
            .GreaterThanOrEqualTo('f').WithMessage(() => A.CustomLocalization.Value)
            .LessThan('b')
            .LessThanOrEqualTo('c');
    }
}