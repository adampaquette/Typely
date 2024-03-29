﻿using Microsoft.CodeAnalysis;
using System.Text.RegularExpressions;
using Typely.Generators.Typely.Emitting;

namespace Typely.Generators.Typely.Parsing.TypeBuilders;

internal class EmittableTypeBuilderOfString : EmittableTypeBuilderBase, IEmittableTypeBuilder
{
    public EmittableTypeBuilderOfString(string defaultNamespace, IEnumerable<ParsedInvocation> invocations,
        SemanticModel model)
        : base(invocations, new EmittableTypeBuilder("string", false, defaultNamespace), model)
    {
    }

    public override EmittableType? Build()
    {
        foreach (var invocation in Invocations)
        {
            switch (invocation.MemberName)
            {
                case TypelyBuilderOf.NotEmptyMethodName:
                    AddRule(
                        errorCode: ErrorCodes.NotEmpty,
                        rule: $"string.IsNullOrWhiteSpace({Emitter.ValueParameterName})",
                        message: ErrorMessageResource.NotEmpty);
                    continue;
                case TypelyBuilderOf.NotEqualMethodName:
                    var value = invocation.GetFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.NotEqual,
                        rule: $"{Emitter.ValueParameterName}.Equals({value})",
                        message: ErrorMessageResource.NotEqual,
                        placeholders: (ValidationPlaceholder.ComparisonValue, value));
                    continue;
                case TypelyBuilderOf.LengthMethodName:
                    if (invocation.ArgumentListSyntax.Arguments.Count == 2)
                    {
                        var min = invocation.GetFirstArgument();
                        var max = invocation.GetSecondArgument();
                        EmittableTypeBuilder.Properties.SetMaxLength(int.Parse(max));
                        AddRule(
                            errorCode: ErrorCodes.Length,
                            rule:
                            $"{Emitter.ValueParameterName}.Length < {min} || {Emitter.ValueParameterName}.Length > {max}",
                            message: ErrorMessageResource.Length,
                            (ValidationPlaceholder.MinLength, min), (ValidationPlaceholder.MaxLength, max));
                    }
                    else
                    {
                        var exactLength = invocation.GetFirstArgument();
                        EmittableTypeBuilder.Properties.SetMaxLength(int.Parse(exactLength));
                        AddRule(
                            errorCode: ErrorCodes.ExactLength,
                            rule: $"{Emitter.ValueParameterName}.Length != {exactLength}",
                            message: ErrorMessageResource.ExactLength,
                            placeholders: (ValidationPlaceholder.ExactLength, exactLength));
                    }

                    continue;
                case TypelyBuilderOf.MinLengthMethodName:
                    value = invocation.GetFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.MinLength,
                        rule: $"{Emitter.ValueParameterName}.Length < {value}",
                        message: ErrorMessageResource.MinLength,
                        placeholders: (ValidationPlaceholder.MinLength, value));
                    continue;
                case TypelyBuilderOf.MaxLengthMethodName:
                    value = invocation.GetFirstArgument();
                    EmittableTypeBuilder.Properties.SetMaxLength(int.Parse(value));
                    AddRule(
                        errorCode: ErrorCodes.MaxLength,
                        rule: $"{Emitter.ValueParameterName}.Length > {value}",
                        message: (ErrorMessageResource.MaxLength),
                        placeholders: (ValidationPlaceholder.MaxLength, value));
                    continue;
                case TypelyBuilderOf.MatchesMethodName:
                    value = invocation.GetFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.Matches,
                        rule: $"!{value}.IsMatch({Emitter.ValueParameterName})",
                        message: (ErrorMessageResource.Matches),
                        placeholders: (ValidationPlaceholder.RegularExpression, value));
                    EmittableTypeBuilder.AdditionalNamespaces.Add("System.Text.RegularExpressions");
                    continue;
                case TypelyBuilderOf.GreaterThanMethodName:
                    value = invocation.GetFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.GreaterThan,
                        rule: $"string.Compare({Emitter.ValueParameterName}, {value}, StringComparison.Ordinal) <= 0",
                        message: (ErrorMessageResource.GreaterThan),
                        placeholders: (ValidationPlaceholder.ComparisonValue, value));
                    continue;
                case TypelyBuilderOf.GreaterThanOrEqualToMethodName:
                    value = invocation.GetFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.GreaterThanOrEqualTo,
                        rule: $"string.Compare({Emitter.ValueParameterName}, {value}, StringComparison.Ordinal) < 0",
                        message: (ErrorMessageResource.GreaterThanOrEqualTo),
                        placeholders: (ValidationPlaceholder.ComparisonValue, value));
                    continue;
                case TypelyBuilderOf.LessThanMethodName:
                    value = invocation.GetFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.LessThan,
                        rule: $"string.Compare({Emitter.ValueParameterName}, {value}, StringComparison.Ordinal) >= 0",
                        message: (ErrorMessageResource.LessThan),
                        placeholders: (ValidationPlaceholder.ComparisonValue, value));
                    continue;
                case TypelyBuilderOf.LessThanOrEqualToMethodName:
                    value = invocation.GetFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.LessThanOrEqualTo,
                        rule: $"string.Compare({Emitter.ValueParameterName}, {value}, StringComparison.Ordinal) > 0",
                        message: (ErrorMessageResource.LessThanOrEqualTo),
                        placeholders: (ValidationPlaceholder.ComparisonValue, value));
                    continue;
            }

            TryHandleInvocation(invocation);
        }

        return EmittableTypeBuilder.TypeName == null ? null : EmittableTypeBuilder.Build();
    }
}