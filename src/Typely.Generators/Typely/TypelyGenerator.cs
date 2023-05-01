using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        var classProvider = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (node, _) => Parser.IsTypelyConfigurationClass(node),
                transform: static (ctx, _) => Parser.GetSemanticTargetForGeneration(ctx))
            .Where(x => x is not null)
            .Select((x, _) => x!)
            .Combine(context.CompilationProvider)
            .WithComparer(new ClassProviderComparer());

        context.RegisterSourceOutput(classProvider, static (spc, source) => Execute(source.Item1!,source.Item2 ,spc));
    }

    private static void Execute(ClassDeclarationSyntax classSyntax, Compilation compilation, SourceProductionContext context)
    {
        var parser = new Parser(compilation, context.ReportDiagnostic, context.CancellationToken);
        var emittableTypes = parser.GetEmittableTypes(classSyntax.SyntaxTree);

        if (!emittableTypes.Any())
        {
            return;
        }

        var emitter = new Emitter(context.ReportDiagnostic, context.CancellationToken);

        foreach (var emittableType in emittableTypes)
        {
            context.CancellationToken.ThrowIfCancellationRequested();
            
            var source = emitter.Emit(emittableType);
            context.AddSource($"{emittableType.Namespace}.{emittableType.TypeName}.g.cs", SourceText.From(source, Encoding.UTF8));
        }
    }
}