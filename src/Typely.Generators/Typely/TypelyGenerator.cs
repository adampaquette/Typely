using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Text;
using Typely.Generators.Typely.Emetting;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Typely;

/// <summary>
/// Generates value object types.
/// </summary>
[Generator]
public partial class TypelyGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (x, _) => Parser.IsSyntaxTargetForGeneration(x),
                transform: static (ctx, _) => Parser.GetSemanticTargetForGeneration(ctx))
            .Where(x => x is not null);

        var compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndClasses, static (spc, source) => Execute(source.Left, source.Right!, spc));
    }

    private static void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes, SourceProductionContext context)
    {
        if (classes.IsDefaultOrEmpty)
        {
            return;
        }

        try
        {
            var distinctClasses = classes.Distinct();
            var parser = new Parser(compilation, context.ReportDiagnostic, context.CancellationToken);
            var emittableTypes = parser.GetEmittableTypes(distinctClasses);

            if (emittableTypes.Count == 0)
            {
                return;
            }

            var emitter = new Emitter(context.ReportDiagnostic, context.CancellationToken);

            foreach (var emittableType in emittableTypes)
            {
                var source = emitter.Emit(emittableType);
                context.AddSource($"{emittableType.TypeName}.g.cs", SourceText.From(source, Encoding.UTF8));
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}