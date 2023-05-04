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
                predicate: Parser.IsTypelyConfigurationClass,
                transform: Parser.GetSemanticTargetForGeneration)
            .Where(x => x is not null)
            .SelectMany(Parser.GetEmittableTypes);
        
        context.RegisterSourceOutput(emittableTypeProvider, AddEmittedSource);
    }
    
    /// <summary>
    /// Emit the source code for a value object and add it to the <see cref="SourceProductionContext"/>.
    /// </summary>
    /// <param name="context">Context for source production.</param>
    /// <param name="emittableType">The type to generate.</param>
    private static void AddEmittedSource(SourceProductionContext context, EmittableType emittableType)
    {
        if (emittableType.Name == null)
        {
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.NameMissing, Location.None,
                emittableType.Namespace));
            return;
        }

        if (emittableType.TypeName == null)
        {
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.TypeNameMissing, Location.None,
                emittableType.Namespace));
            return;
        }

        var source = Emitter.Emit(emittableType, context.CancellationToken);

        context.AddSource($"{emittableType.Namespace}.{emittableType.TypeName}.g.cs",
            SourceText.From(source, Encoding.UTF8));
    }
}