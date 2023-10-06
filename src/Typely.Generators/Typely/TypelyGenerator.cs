using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using Typely.Generators.Typely.Emitting;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Typely;

/// <summary>
/// Generates value object types.
/// </summary>
[Generator]
public class TypelyGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var emittableTypeProvider = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: Parser.IsTypelySpecificationClass,
                transform: Parser.GetEmittableTypesForClass)
            .Where(x => x is not null)
            .SelectMany((emittableTypes, _) => emittableTypes!);
        
        context.RegisterSourceOutput(emittableTypeProvider, AddEmittedSource);
    }
    
    /// <summary>
    /// Emit the source code for a value object and add it to the <see cref="SourceProductionContext"/>.
    /// </summary>
    /// <param name="context">Context for source production.</param>
    /// <param name="emittableType">The type to generate.</param>
    private static void AddEmittedSource(SourceProductionContext context, EmittableType emittableType)
    {
        context.CancellationToken.ThrowIfCancellationRequested();
        
        var source = Emitter.Emit(emittableType);

        context.AddSource($"{emittableType.Namespace}.{emittableType.TypeName}.g.cs",
            SourceText.From(source, Encoding.UTF8));
    }
}