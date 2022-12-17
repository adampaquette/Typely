using FluentType.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using FluentType.SourceGenerators.Extensions;

namespace FluentType.Generators;

public partial class FluentTypeGenerator
{
    internal class Parser
    {
        private readonly CancellationToken _cancellationToken;
        private readonly Compilation _compilation;
        private readonly Action<Diagnostic> _reportDiagnostic;

        public Parser(Compilation compilation, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
        {
            _compilation = compilation;
            _cancellationToken = cancellationToken;
            _reportDiagnostic = reportDiagnostic;
        }

        internal static bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode) =>
        syntaxNode is ClassDeclarationSyntax c && c.HasInterface(nameof(IFluentTypesConfiguration));

        internal static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
        {
            var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;
            var classSymbol = context.SemanticModel.GetSymbolInfo(classDeclarationSyntax).Symbol;
            if (classSymbol == null)
            {
                return null;
            }

            //https://weblog.west-wind.com/posts/2022/Jun/07/Runtime-CSharp-Code-Compilation-Revisited-for-Roslyn
            //https://andrewlock.net/exploring-dotnet-6-part-9-source-generator-updates-incremental-generators/
            return classDeclarationSyntax;
        }

        public IReadOnlyList<FluentTypeModel> GetFluentTypes(IEnumerable<ClassDeclarationSyntax> classes) 
        {
            return Array.Empty<FluentTypeModel>();
        }
    }

    internal class FluentTypeModel
    { 
    }
}
