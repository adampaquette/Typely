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

        context.RegisterSourceOutput(compilationAndClasses, static (spc, source) => Execute(source.Left, source.Right, spc));
    }

    private static void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes, SourceProductionContext context)
    {
        if (classes.IsDefaultOrEmpty)
        {
            return;
        }

        var distinctClasses = classes.Distinct();       
        var parser = new Parser(compilation, context.ReportDiagnostic, context.CancellationToken);
        var fluentTypes = parser.GetFluentTypes(distinctClasses);
        
        if (fluentTypes.Count > 0)
        {
            var emitter = new Emitter();
            var source = emitter.Emit(fluentTypes, context.CancellationToken);

            context.AddSource("FluentTypes.g.cs", SourceText.From(source, Encoding.UTF8));
        }
    }
}
