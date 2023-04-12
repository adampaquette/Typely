using Typely.Core;
using Typely.Core.Builders;
using Typely.Generators.Typely.Emetting;

namespace Typely.Generators.Typely.Parsing.TypeBuilders;

internal class EmittableTypeBuilderOfInt : EmittableTypeBuilderBase, IEmittableTypeBuilder
{
    public EmittableTypeBuilderOfInt(string defaultNamespace, IEnumerable<ParsedInvocation> invocations)
        : base(invocations, new EmittableType(typeof(int), defaultNamespace))
    {
    }

    public EmittableType Build()
    {
        foreach (var invocation in Invocations)
        {
            if(TryHandleInvocation(invocation))
            {
                continue;
            }
            
            var value = invocation.GetFirstStringArgument();
            
            switch (invocation.MemberName)
            {
                case nameof(ITypelyBuilderOfInt.Must):
                    AddRule(
                        errorCode: ErrorCodes.Must,
                        rule: $"!({value})",
                        message: nameof(ErrorMessages.Must));
                    break;
                case nameof(ITypelyBuilderOfInt.GreaterThan):
                    AddRule(
                        errorCode: ErrorCodes.GreaterThan,
                        rule: $"{Emitter.ValueParameterName} <= {value}",
                        message: nameof(ErrorMessages.GreaterThan),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    break;
                case nameof(ITypelyBuilderOfInt.GreaterThanOrEqualTo):
                    AddRule(
                        errorCode: ErrorCodes.GreaterThanOrEqualTo,
                        rule: $"{Emitter.ValueParameterName} < {value}",
                        message: nameof(ErrorMessages.GreaterThanOrEqualTo),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    break;
                case nameof(ITypelyBuilderOfInt.LessThan):
                    AddRule(
                        errorCode: ErrorCodes.LessThan,
                        rule: $"{Emitter.ValueParameterName} >= {value}",
                        message: nameof(ErrorMessages.LessThan),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    break;
                case nameof(ITypelyBuilderOfInt.LessThanOrEqualTo):
                    AddRule(
                        errorCode: ErrorCodes.LessThanOrEqualTo,
                        rule: $"{Emitter.ValueParameterName} > {value}",
                        message: nameof(ErrorMessages.LessThanOrEqualTo),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    break;
                case nameof(ITypelyBuilderOfInt.NotEmpty):
                    AddRule(
                        errorCode: ErrorCodes.NotEmpty,
                        rule: $"{Emitter.ValueParameterName} == 0",
                        message: nameof(ErrorMessages.NotEmpty));
                    break;
                case nameof(ITypelyBuilderOfInt.NotEqual):
                    AddRule(
                        errorCode: ErrorCodes.NotEqual,
                        rule: $"{Emitter.ValueParameterName}.Equals({value})",
                        message: nameof(ErrorMessages.NotEqual),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    break;
                default: throw new NotSupportedException(invocation.MemberName);
            }
        }

        return EmittableType;
    }
}