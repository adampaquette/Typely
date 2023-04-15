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
            case nameof(ITypelyBuilder.OfString):
                return new EmittableTypeBuilderOfString(defaultNamespace, invocations, model);
            case nameof(ITypelyBuilder.OfInt):
                return new EmittableTypeBuilderOfInt(defaultNamespace, invocations, model);
            default: throw new InvalidOperationException($"Unknown builder type: {builderType}");
        }
    }
}