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
        var emittableTypeOrDiagnosticProvider = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: Parser.IsTypelySpecificationClass,
                transform: Parser.GetEmittableTypesAndDiagnosticsForClass)
            .Where(x => x is not null)
            .SelectMany((emittableTypes, _) => emittableTypes!);

        var diagnosticProvider = emittableTypeOrDiagnosticProvider.Where(x => x.Diagnostic is not null)
            .Select(selector: (x, _) => x.Diagnostic!);

        context.RegisterSourceOutput(diagnosticProvider, AddDiagnostic);
        
        var emittableTypesProvider = emittableTypeOrDiagnosticProvider.Where(x => x.EmittableType is not null)
            .Select(selector: (x, _) => x.EmittableType!);
        
        context.RegisterSourceOutput(emittableTypesProvider, AddEmittedSource);
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
    
    private static void AddDiagnostic(SourceProductionContext context, Diagnostic diagnostic)
    {
        context.CancellationToken.ThrowIfCancellationRequested();

        context.ReportDiagnostic(diagnostic);
    }
}