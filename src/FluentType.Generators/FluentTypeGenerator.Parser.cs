using FluentType.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using FluentType.SourceGenerators.Extensions;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using FluentType.Generators.Extensions;
using System.Linq.Expressions;

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

        /// <summary>
        /// Filter classes having an interface name <see cref="IFluentTypesConfiguration"/>.
        /// </summary>
        internal static bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode) =>
            syntaxNode is ClassDeclarationSyntax c && c.HasInterface(nameof(IFluentTypesConfiguration));

        /// <summary>
        /// Filter classes having an interface <see cref="IFluentTypesConfiguration"/> that matches the 
        /// namespace and returns the <see cref="ClassDeclarationSyntax"/>.
        /// </summary>
        internal static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
        {
            var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;
            var classSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax) as INamedTypeSymbol;
            if (classSymbol == null)
            {
                return null;
            }
            return classSymbol.AllInterfaces.Any(x => x.ToString() == typeof(IFluentTypesConfiguration).FullName) ?
                classDeclarationSyntax : null;
        }

        public IReadOnlyList<FluentTypeModel> GetFluentTypes(IEnumerable<ClassDeclarationSyntax> classes)
        {
            // We enumerate by syntax tree, to minimize impact on performance
            foreach (var group in classes.GroupBy(x => x.SyntaxTree))
            {
                // Stop if we're asked to
                _cancellationToken.ThrowIfCancellationRequested();

                var syntaxTree = group.Key;
                var compilation = CSharpCompilation.Create(assemblyName: Path.GetRandomFileName())
                    .WithReferences(typeof(object), typeof(IFluentTypesConfiguration), typeof(Enumerable), typeof(Expression))
                    .WithDefaultNetCoreReferences()
                    .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                    .AddSyntaxTrees(syntaxTree);

                var refs = compilation.References.ToList();

                using (var ms = new MemoryStream())
                {
                    var result = compilation.Emit(ms);
                    if (!result.Success)
                    {
                        return Array.Empty<FluentTypeModel>();
                    }

                    ms.Seek(0, SeekOrigin.Begin);
                    var assembly = Assembly.Load(ms.ToArray());

                    try
                    {
                        var types = assembly.GetTypes();
                    }
                    catch (Exception ex)
                    {
                        var a = ex;

                    }


                    dynamic fluentTypeConfiguration = assembly.CreateInstance("FluentType.Playground.TypesConfiguration");

                    var fluentBuilder = new FluentTypeBuilder();
                    fluentTypeConfiguration.Configure();
                    var called = fluentBuilder.Called;
                }
            }

            return Array.Empty<FluentTypeModel>();
        }
    }

    internal class FluentTypeModel
    {
    }

    internal class FluentTypeBuilder : IFluentTypeBuilder
    {
        public int Called { get; set; }

        public IFluentTypeBuilder<T> For<T>(string typeName)
        {
            Called++;
            return default;
        }
    }
}
