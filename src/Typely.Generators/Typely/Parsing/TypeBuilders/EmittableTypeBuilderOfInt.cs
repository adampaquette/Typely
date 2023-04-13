using Microsoft.CodeAnalysis;
using Typely.Core;
using Typely.Core.Builders;
using Typely.Generators.Typely.Emitting;

namespace Typely.Generators.Typely.Parsing.TypeBuilders;

internal class EmittableTypeBuilderOfInt : EmittableTypeBuilderBase, IEmittableTypeBuilder
{
    public EmittableTypeBuilderOfInt(string defaultNamespace, IEnumerable<ParsedInvocation> invocations, SemanticModel model)
        : base(invocations, new EmittableType(typeof(int), defaultNamespace), model)
    {
    }

    public EmittableType Build()
    {
        foreach (var invocation in Invocations)
        {
            if (TryHandleInvocation(invocation))
            {
                continue;
            }

            switch (invocation.MemberName)
            {
                case nameof(ITypelyBuilderOfInt.NotEmpty):
                    AddRule(
                        errorCode: ErrorCodes.NotEmpty,
                        rule: $"{Emitter.ValueParameterName} == 0",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.NotEmpty));
                    break;
                case nameof(ITypelyBuilderOfInt.Must):
                    var value = invocation.GetLambdaBodyOfFirstArgument();
                    AddRule(
                        errorCode: ErrorCodes.Must,
                        rule: $"!({value})",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.Must));
                    break;
                case nameof(ITypelyBuilderOfInt.GreaterThan):
                    value = invocation.GetFirstNumberArgument();
                    AddRule(
                        errorCode: ErrorCodes.GreaterThan,
                        rule: $"{Emitter.ValueParameterName} <= {value}",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.GreaterThan),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    break;
                case nameof(ITypelyBuilderOfInt.GreaterThanOrEqualTo):
                    value = invocation.GetFirstNumberArgument();
                    AddRule(
                        errorCode: ErrorCodes.GreaterThanOrEqualTo,
                        rule: $"{Emitter.ValueParameterName} < {value}",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.GreaterThanOrEqualTo),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    break;
                case nameof(ITypelyBuilderOfInt.LessThan):
                    value = invocation.GetFirstNumberArgument();
                    AddRule(
                        errorCode: ErrorCodes.LessThan,
                        rule: $"{Emitter.ValueParameterName} >= {value}",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.LessThan),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    break;
                case nameof(ITypelyBuilderOfInt.LessThanOrEqualTo):
                    value = invocation.GetFirstNumberArgument();
                    AddRule(
                        errorCode: ErrorCodes.LessThanOrEqualTo,
                        rule: $"{Emitter.ValueParameterName} > {value}",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.LessThanOrEqualTo),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    break;
                case nameof(ITypelyBuilderOfInt.NotEqual):
                    value = invocation.GetFirstNumberArgument();
                    AddRule(
                        errorCode: ErrorCodes.NotEqual,
                        rule: $"{Emitter.ValueParameterName}.Equals({value})",
                        message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.NotEqual),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    break;
                default: throw new NotSupportedException(invocation.MemberName);
            }
        }

        return EmittableType;
    }
}