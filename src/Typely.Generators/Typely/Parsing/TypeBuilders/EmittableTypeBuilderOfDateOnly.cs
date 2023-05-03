using Microsoft.CodeAnalysis;

namespace Typely.Generators.Typely.Parsing.TypeBuilders;

internal class EmittableTypeBuilderOfDateOnly : EmittableTypeBuilderBase, IEmittableTypeBuilder
{
    public EmittableTypeBuilderOfDateOnly(string defaultNamespace, IEnumerable<ParsedInvocation> invocations, SemanticModel model)
        : base(invocations, new EmittableTypeBuilder("DateOnly", true, defaultNamespace), model)
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