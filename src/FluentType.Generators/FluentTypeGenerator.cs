using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Text;

namespace FluentType.Generators;

[Generator]
public partial class FluentTypeGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (x, _) => Parser.IsSyntaxTargetForGeneration(x),
                transform: static (ctx, _) => Parser.GetSemanticTargetForGeneration(ctx))
            .Where(x => x is not null);

        var compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndClasses, static (spc, source) => Execute(source.Item1, source.Item2, spc));
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
            var p = new Parser(compilation, context.ReportDiagnostic, context.CancellationToken);

            var fluentTypes = p.GetFluentTypes(distinctClasses);
            if (fluentTypes.Count > 0)
            {
                var e = new Emitter();
                var source = e.Emit(fluentTypes, context.CancellationToken);

                context.AddSource("FluentTypes.g.cs", SourceText.From(source, Encoding.UTF8));
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
