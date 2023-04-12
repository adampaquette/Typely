using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing.TypeBuilders;

internal static class EmittableTypeBuilderFactory
{
    public static IEmittableTypeBuilder Create(string defaultNamespace,
        ParsedExpressionStatement parsedExpressionStatement)
    {
        if (parsedExpressionStatement.Invocations.Count == 0)
        {
            throw new InvalidOperationException("MemberAccess cannot be empty");
        }

        var builderType = parsedExpressionStatement.Invocations.First().MemberName;
        var invocations = parsedExpressionStatement.Invocations.Skip(1);

        switch (builderType)
        {
            case nameof(ITypelyBuilder.OfString):
                return new EmittableTypeBuilderOfString(defaultNamespace, invocations);
            case nameof(ITypelyBuilder.OfInt):
                return new EmittableTypeBuilderOfInt(defaultNamespace, invocations);
            default: throw new InvalidOperationException($"Unknown builder type: {builderType}");
        }
    }
}