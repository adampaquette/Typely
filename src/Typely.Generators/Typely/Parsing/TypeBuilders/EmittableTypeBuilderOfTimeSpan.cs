using Microsoft.CodeAnalysis;

namespace Typely.Generators.Typely.Parsing.TypeBuilders;

internal class EmittableTypeBuilderOfTimeSpan : EmittableTypeBuilderBase, IEmittableTypeBuilder
{
    public EmittableTypeBuilderOfTimeSpan(string defaultNamespace, IEnumerable<ParsedInvocation> invocations, SemanticModel model)
        : base(invocations, new EmittableTypeBuilder("TimeSpan", true, defaultNamespace), model)
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