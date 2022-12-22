using FluentType.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using FluentType.SourceGenerators.Extensions;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using FluentType.Generators.Extensions;
using Basic.Reference.Assemblies;

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
            var classSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax);
            if (classSymbol == null)
            {
                return null;
            }
            return classSymbol.AllInterfaces.Any(x => x.ToString() == typeof(IFluentTypesConfiguration).FullName) ?
                classDeclarationSyntax : null;
        }

        public IReadOnlyList<FluentTypeModel> GetFluentTypes(IEnumerable<ClassDeclarationSyntax> classes)
        {
            try
            {
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

                // We enumerate by syntax tree, to minimize impact on performance
                foreach (var group in classes.GroupBy(x => x.SyntaxTree))
                {
                    // Stop if we're asked to
                    _cancellationToken.ThrowIfCancellationRequested();

                    var configurationAssembly = CompileUserCodeTypeConfiguration(group.Key);
                    var fluentTypesConfigurationTypes = configurationAssembly.GetTypes()
                        .Where(x => x.GetInterfaces().Contains(typeof(IFluentTypesConfiguration)))
                        .ToList();

                    foreach (var fluentTypesConfigurationType in fluentTypesConfigurationTypes)
                    {
                        var fluentTypeConfiguration = (IFluentTypesConfiguration)configurationAssembly.CreateInstance(fluentTypesConfigurationType.FullName);
                        var fluentBuilder = new FluentTypeBuilder();
                        fluentTypeConfiguration.Configure(fluentBuilder);
                        var called = fluentBuilder.Called;
                    }
                }
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
            }

            return Array.Empty<FluentTypeModel>();
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) =>
            AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName == args.Name);

        private Assembly? CompileUserCodeTypeConfiguration(SyntaxTree syntaxTree)
        {
            var compilation = CSharpCompilation.Create(assemblyName: $"FluentType_{Path.GetRandomFileName()}")
                .WithReferenceAssemblies(ReferenceAssemblyKind.NetStandard20)
                .AddReferences(typeof(IFluentTypesConfiguration))
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddSyntaxTrees(syntaxTree);

            using var ms = new MemoryStream();
            var result = compilation.Emit(ms);
            if (!result.Success)
            {
                //TODO : DIAGNOSTICS
                return null;
            }

            ms.Seek(0, SeekOrigin.Begin);
            return Assembly.Load(ms.ToArray());
        }
    }

    internal class FluentTypeModel
    {
    }

    public class FluentTypeBuilder : IFluentTypeBuilder
    {
        public int Called { get; set; }

        public IFluentTypeBuilder<T> For<T>(string typeName)
        {
            Called++;
            return default;
        }
    }
}
