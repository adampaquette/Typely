using Microsoft.CodeAnalysis;

namespace Typely.Generators.Typely.Parsing.TypeBuilders;

internal class EmittableTypeBuilderOfByte : EmittableTypeBuilderBase, IEmittableTypeBuilder
{
    public EmittableTypeBuilderOfByte(string defaultNamespace, IEnumerable<ParsedInvocation> invocations, SemanticModel model)
        : base(invocations, new EmittableTypeBuilder("byte", true, defaultNamespace), model)
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