using FluentType.Core;
using FluentType.SourceGenerators.Commun;
using Microsoft.CodeAnalysis;

namespace FluentType.SourceGenerators.Generators;

[Generator]
public class FluentTypeGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new ImplementsInterfaceSyntaxReceiver<IFluentTypesConfiguration>());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        
    }
}
