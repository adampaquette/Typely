using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing.TypeBuilders;

internal static class EmittableTypeBuilderFactory
{
    public static IEmittableTypeBuilder Create(string defaultNamespace,
        ParsedStatement parsedStatement)
    {
        var builderType = parsedStatement.Invocations.First().MemberName;
        var invocations = parsedStatement.Invocations.Skip(1);
        var model = parsedStatement.SemanticModel;

        switch (builderType)
        {
            case nameof(ITypelyBuilder.OfBool):
                return new EmittableTypeBuilderOfBool(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfByte):
                return new EmittableTypeBuilderOfByte(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfChar):
                return new EmittableTypeBuilderOfChar(defaultNamespace, invocations, model);
            case "OfDateOnly":
                return new EmittableTypeBuilderOfDateOnly(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfDateTime):
                return new EmittableTypeBuilderOfDateTime(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfDateTimeOffset):
                return new EmittableTypeBuilderOfDateTimeOffset(defaultNamespace, invocations, model);

            case nameof(ITypelyBuilder.OfInt):
                return new EmittableTypeBuilderOfInt(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfDecimal):
                return new EmittableTypeBuilderOfDecimal(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfFloat):
                return new EmittableTypeBuilderOfFloat(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfGuid):
                return new EmittableTypeBuilderOfGuid(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfDouble):
                return new EmittableTypeBuilderOfDouble(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfLong):
                return new EmittableTypeBuilderOfLong(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfSByte):
                return new EmittableTypeBuilderOfSByte(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfShort):
                return new EmittableTypeBuilderOfShort(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfString):
                return new EmittableTypeBuilderOfString(defaultNamespace, invocations, model);
            case "OfTimeOnly":
                return new EmittableTypeBuilderOfTimeOnly(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfTimeSpan):
                return new EmittableTypeBuilderOfTimeSpan(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfUInt):
                return new EmittableTypeBuilderOfUint(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfULong):
                return new EmittableTypeBuilderOfULong(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfUShort):
                return new EmittableTypeBuilderOfUShort(defaultNamespace, invocations, model);
            default: throw new InvalidOperationException($"Unknown builder type: {builderType}");
        }
    }
}