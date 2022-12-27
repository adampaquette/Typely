using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Text;

namespace Typely.Generators;

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

        var distinctClasses = classes.Distinct();
        var parser = new Parser(compilation, context.ReportDiagnostic, context.CancellationToken);
        var Typelys = parser.GetTypelys(distinctClasses);

        if (Typelys.Count == 0)
        {
            return;
        }

        var emitter = new Emitter();

        foreach (var Typely in Typelys)
        {
            var source = emitter.Emit(Typely);
            context.AddSource($"{Typely.Name}.g.cs", SourceText.From(source, Encoding.UTF8));
        }
    }
}
