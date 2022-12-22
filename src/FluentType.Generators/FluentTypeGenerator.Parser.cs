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

                //var syntaxTree = group.Key;

                var source = """
                using System;
                using FluentType.Core;                

                namespace Test;

                public class A : IFluentTypeBuilder
                {
                }
                """;
                var syntaxTree = SyntaxFactory.ParseSyntaxTree(source);

               

                var compilation = CSharpCompilation.Create(assemblyName: Path.GetRandomFileName())
                    .WithReferenceAssemblies(ReferenceAssemblyKind.NetStandard20)
                    //.AddReferences(typeof(IFluentTypesConfiguration))
                    .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                    .AddSyntaxTrees(syntaxTree);

                var types2 = typeof(IFluentTypesConfiguration).Assembly.GetTypes();

                compilation = compilation.AddReferences(MetadataReference.CreateFromFile("C:\\Users\\nfs12\\source\\repos\\FluentType\\src\\FluentType.Generators\\FluentType.Core.dll"));
                //compilation = compilation.AddReferences(MetadataReference.CreateFromFile(typeof(System.Runtime.CompilerServices.AccessedThroughPropertyAttribute).Assembly.Location));

                var a = typeof(IFluentTypesConfiguration).Assembly.Location;

                var refs = compilation.References.ToList();

                ////DOMAIN ASSEMBLIES
                //var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                //    .Where(x => !x.IsDynamic)
                //    .Select(x => MetadataReference.CreateFromFile(x.Location))
                //    .ToList();
                //compilation = compilation.AddReferences(domainAssemblies);

                ////REFERENCE ASSEMBLIES
                //var netstandardFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget\\packages\\netstandard.library\\2.0.3\\build\\netstandard2.0\\ref");
                //var referenceAssemblies = Directory.GetFiles(netstandardFolder)
                //    .Where(x => x.EndsWith(".dll"))
                //    .Select(p => MetadataReference.CreateFromFile(p))
                //    .ToList();
                //compilation = compilation.AddReferences(referenceAssemblies);

                ////IMPLEMENTATION ASSEMBLIES
                //var implementationAssemblies = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")).Split(Path.PathSeparator);
                //var references = implementationAssemblies
                //    .Select(p => MetadataReference.CreateFromFile(p))
                //    .ToList();
                //compilation = compilation.AddReferences(references);     

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
                        dynamic fluentTypeConfiguration = assembly.CreateInstance("Test.TypesConfiguration");

                        //var fluentBuilder = new FluentTypeBuilder();
                        //fluentTypeConfiguration.Configure();
                        //var called = fluentBuilder.Called;
                    }
                    catch (Exception ex)
                    {
                        var gga = ex;
                    }
                }
            }

            return Array.Empty<FluentTypeModel>();
        }
    }

    internal class FluentTypeModel
    {
    }

    //internal class FluentTypeBuilder : IFluentTypeBuilder
    //{
    //    public int Called { get; set; }

    //    public IFluentTypeBuilder<T> For<T>(string typeName)
    //    {
    //        Called++;
    //        return default;
    //    }
    //}
}
