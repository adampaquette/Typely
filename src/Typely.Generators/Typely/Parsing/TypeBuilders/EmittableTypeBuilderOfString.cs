using Microsoft.CodeAnalysis;
using Typely.Core;
using Typely.Core.Builders;
using Typely.Generators.Typely.Emitting;

namespace Typely.Generators.Typely.Parsing.TypeBuilders;

internal class EmittableTypeBuilderOfString : EmittableTypeBuilderBase, IEmittableTypeBuilder
{
    public EmittableTypeBuilderOfString(string defaultNamespace, IEnumerable<ParsedInvocation> invocations,
        SemanticModel model)
        : base(invocations, new EmittableType("string", false, defaultNamespace), model)
    {
    }

    public EmittableType Build()
    {
        foreach (var invocation in Invocations)
        {
            switch (invocation.MemberName)
            {
                case nameof(ITypelyBuilderOfInt.NotEmpty):
                    AddRule(
                        errorCode: ErrorCodes.NotEmpty,
                        rule: $"string.IsNullOrWhiteSpace({Emitter.ValueParameterName})",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.NotEmpty));
                    continue;
                case nameof(ITypelyBuilderOfInt.NotEqual):
                    var value = invocation.GetFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.NotEqual,
                        rule: $"{Emitter.ValueParameterName}.Equals({value})",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.NotEqual),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    continue;
                case nameof(ITypelyBuilderOfString.Length):
                    if (invocation.ArgumentListSyntax.Arguments.Count == 2)
                    {
                        var min = invocation.GetFirstArgument();
                        var max = invocation.GetSecondArgument();
                        AddRule(
                            errorCode: ErrorCodes.Length,
                            rule: $"{Emitter.ValueParameterName}.Length < {min} || {Emitter.ValueParameterName}.Length > {max}",
                            message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.Length),
                            (ValidationPlaceholders.MinLength, min), (ValidationPlaceholders.MaxLength, max));
                    }
                    else
                    {
                        var exactLength = invocation.GetFirstArgument();
                        AddRule(
                            errorCode: ErrorCodes.ExactLength,
                            rule: $"{Emitter.ValueParameterName}.Length != {exactLength}",
                            message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.ExactLength),
                            placeholders: (ValidationPlaceholders.ExactLength, exactLength));
                    }

                    continue;
                case nameof(ITypelyBuilderOfString.MinLength):
                    value = invocation.GetFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.MinLength,
                        rule: $"{Emitter.ValueParameterName}.Length < {value}",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.MinLength),
                        placeholders: (ValidationPlaceholders.MinLength, value));
                    continue;
                case nameof(ITypelyBuilderOfString.MaxLength):
                    value = invocation.GetFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.MaxLength,
                        rule: $"{Emitter.ValueParameterName}.Length > {value}",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.MaxLength),
                        placeholders: (ValidationPlaceholders.MaxLength, value));
                    continue;
                case nameof(ITypelyBuilderOfString.Matches):
                    value = invocation.GetFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.Matches,
                        rule: $"!{value}.IsMatch({Emitter.ValueParameterName})",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.Matches),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    EmittableType.AdditionalNamespaces.Add("System.Text.RegularExpressions");
                    continue;
                case nameof(ITypelyBuilderOfInt.GreaterThan):
                    value = invocation.GetFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.GreaterThan,
                        rule: $"string.Compare({Emitter.ValueParameterName}, {value}, StringComparison.Ordinal) <= 0",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.GreaterThan),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    continue;
                case nameof(ITypelyBuilderOfInt.GreaterThanOrEqualTo):
                    value = invocation.GetFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.GreaterThanOrEqualTo,
                        rule: $"string.Compare({Emitter.ValueParameterName}, {value}, StringComparison.Ordinal) < 0",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.GreaterThanOrEqualTo),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    continue;
                case nameof(ITypelyBuilderOfInt.LessThan):
                    value = invocation.GetFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.LessThan,
                        rule: $"string.Compare({Emitter.ValueParameterName}, {value}, StringComparison.Ordinal) >= 0",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.LessThan),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    continue;
                case nameof(ITypelyBuilderOfInt.LessThanOrEqualTo):
                    value = invocation.GetFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.LessThanOrEqualTo,
                        rule: $"string.Compare({Emitter.ValueParameterName}, {value}, StringComparison.Ordinal) > 0",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.LessThanOrEqualTo),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    continue;
            }

            if (TryHandleInvocation(invocation))
            {
                continue;
            }

            throw new NotSupportedException(invocation.MemberName);
        }

        return EmittableType;
    }
}