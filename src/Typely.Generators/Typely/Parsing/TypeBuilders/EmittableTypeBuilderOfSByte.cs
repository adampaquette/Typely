using Microsoft.CodeAnalysis;

namespace Typely.Generators.Typely.Parsing.TypeBuilders;

internal class EmittableTypeBuilderOfSByte : EmittableTypeBuilderBase, IEmittableTypeBuilder
{
    public EmittableTypeBuilderOfSByte(string defaultNamespace, IEnumerable<ParsedInvocation> invocations, SemanticModel model)
        : base(invocations, new EmittableTypeBuilder("sbyte", true, defaultNamespace), model)
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

            throw new NotSupportedException(invocation.MemberName);
        }

        return EmittableTypeBuilder.Build();
    }
}