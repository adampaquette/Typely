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
            case TypelyBuilder.OfBool:
                return new EmittableTypeBuilderOfBool(defaultNamespace, invocations, model);
            case TypelyBuilder.OfByte:
                return new EmittableTypeBuilderOfByte(defaultNamespace, invocations, model);
            case TypelyBuilder.OfChar:
                return new EmittableTypeBuilderOfChar(defaultNamespace, invocations, model);
            case TypelyBuilder.OfDateOnly:
                return new EmittableTypeBuilderOfDateOnly(defaultNamespace, invocations, model);
            case TypelyBuilder.OfDateTime:
                return new EmittableTypeBuilderOfDateTime(defaultNamespace, invocations, model);
            case TypelyBuilder.OfDateTimeOffset:
                return new EmittableTypeBuilderOfDateTimeOffset(defaultNamespace, invocations, model);
            case TypelyBuilder.OfInt:
                return new EmittableTypeBuilderOfInt(defaultNamespace, invocations, model);
            case TypelyBuilder.OfDecimal:
                return new EmittableTypeBuilderOfDecimal(defaultNamespace, invocations, model);
            case TypelyBuilder.OfFloat:
                return new EmittableTypeBuilderOfFloat(defaultNamespace, invocations, model);
            case TypelyBuilder.OfGuid:
                return new EmittableTypeBuilderOfGuid(defaultNamespace, invocations, model);
            case TypelyBuilder.OfDouble:
                return new EmittableTypeBuilderOfDouble(defaultNamespace, invocations, model);
            case TypelyBuilder.OfLong:
                return new EmittableTypeBuilderOfLong(defaultNamespace, invocations, model);
            case TypelyBuilder.OfSByte:
                return new EmittableTypeBuilderOfSByte(defaultNamespace, invocations, model);
            case TypelyBuilder.OfShort:
                return new EmittableTypeBuilderOfShort(defaultNamespace, invocations, model);
            case TypelyBuilder.OfString:
                return new EmittableTypeBuilderOfString(defaultNamespace, invocations, model);
            case TypelyBuilder.OfTimeOnly:
                return new EmittableTypeBuilderOfTimeOnly(defaultNamespace, invocations, model);
            case TypelyBuilder.OfTimeSpan:
                return new EmittableTypeBuilderOfTimeSpan(defaultNamespace, invocations, model);
            case TypelyBuilder.OfUInt:
                return new EmittableTypeBuilderOfUint(defaultNamespace, invocations, model);
            case TypelyBuilder.OfULong:
                return new EmittableTypeBuilderOfULong(defaultNamespace, invocations, model);
            case TypelyBuilder.OfUShort:
                return new EmittableTypeBuilderOfUShort(defaultNamespace, invocations, model);
            default: throw new InvalidOperationException($"Unknown builder type: {builderType}");
        }
    }
}