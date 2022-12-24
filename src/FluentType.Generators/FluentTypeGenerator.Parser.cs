using FluentType.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using FluentType.SourceGenerators.Extensions;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using FluentType.Generators.Extensions;
using Basic.Reference.Assemblies;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using static FluentType.Generators.FluentTypeGenerator;

namespace FluentType.Generators;

public partial class FluentTypeGenerator
{
    internal sealed class Parser : IDisposable
    {
        private readonly CancellationToken _cancellationToken;
        private readonly Compilation _compilation;
        private readonly Action<Diagnostic> _reportDiagnostic;

        public Parser(Compilation compilation, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
        {
            _compilation = compilation;
            _cancellationToken = cancellationToken;
            _reportDiagnostic = reportDiagnostic;

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
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

        /// <summary>
        /// Execute the different <see cref="IFluentTypesConfiguration"/> classes founds and generate models of the desired user types.
        /// </summary>
        /// <param name="classes">Classes to parse.</param>
        /// <returns>A list of representation of desired user types.</returns>
        public IReadOnlyList<FluentTypeConfiguration> GetFluentTypes(IEnumerable<ClassDeclarationSyntax> classes)
        {
            // We enumerate by syntax tree, to minimize impact on performance
            return classes.GroupBy(x => x.SyntaxTree).SelectMany(x => GetFluentTypes(x.Key)).ToList().AsReadOnly();
        }

        /// <summary>
        /// Execute the different <see cref="IFluentTypesConfiguration"/> classes and generate models of the desired user types.
        /// </summary>
        /// <param name="syntaxTree">SyntaxTree to parse</param>
        /// <returns>A list of representation of desired user types.</returns>
        private IEnumerable<FluentTypeConfiguration> GetFluentTypes(SyntaxTree syntaxTree)
        {
            // Stop if we're asked to
            _cancellationToken.ThrowIfCancellationRequested();

            var configurationAssembly = CreateConfigurationAssembly(syntaxTree);
            if (configurationAssembly == null)
            {
                return Array.Empty<FluentTypeConfiguration>();
            }

            var configurationTypes = configurationAssembly.GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IFluentTypesConfiguration)))
                .ToList();

            foreach (var configurationType in configurationTypes)
            {
                var fluentTypeConfiguration = (IFluentTypesConfiguration)configurationAssembly.CreateInstance(configurationType.FullName);
                var fluentBuilder = new FluentTypesBuilder(syntaxTree);
                fluentTypeConfiguration.Configure(fluentBuilder);
                var called = fluentBuilder.GetFluentTypeConfigurations();
            }

            return Array.Empty<FluentTypeConfiguration>();
        }

        /// <summary>
        /// Only resolve known assemblies. 
        /// </summary>
        private Assembly? CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) =>
            args.Name == "FluentType.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                ? AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName == args.Name)
                : null;

        /// <summary>
        /// Compiles the user's code.
        /// </summary>
        private Assembly? CreateConfigurationAssembly(SyntaxTree syntaxTree)
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
                foreach (var diagnostic in result.Diagnostics)
                {
                    _reportDiagnostic(diagnostic);
                }
                return null;
            }

            ms.Seek(0, SeekOrigin.Begin);
            return Assembly.Load(ms.ToArray());
        }

        private void Diag(DiagnosticDescriptor desc, Location? location, params object?[]? messageArgs)
        {
            _reportDiagnostic(Diagnostic.Create(desc, location, messageArgs));
        }

        public void Dispose()
        {
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
            GC.SuppressFinalize(this);
        }
    }

    //Diag(new DiagnosticDescriptor(
    //                id: "AD",
    //                title: "my title",
    //                messageFormat: "my message",
    //                category: "category",
    //                DiagnosticSeverity.Error,
    //                isEnabledByDefault: true), null, null);

    public class FluentTypesBuilder : IFluentTypesBuilder
    {
        private List<FluentTypeConfiguration> _typeConfigurations = new List<FluentTypeConfiguration>();
        private SyntaxTree _syntaxTree;

        public FluentTypesBuilder(SyntaxTree syntaxTree)
        {
            _syntaxTree = syntaxTree;
        }

        public IFluentTypeBuilder<T> For<T>(string typeName)
        {
            var fluentTypeConfiguration = new FluentTypeConfiguration
            {
                UnderlyingType = typeof(T),
                SyntaxTree = _syntaxTree,
                Name = typeName
            };
            _typeConfigurations.Add(fluentTypeConfiguration);

            return new RuleBuilder<T>(fluentTypeConfiguration);
        }

        public IReadOnlyList<FluentTypeConfiguration> GetFluentTypeConfigurations() =>
            _typeConfigurations.ToList().AsReadOnly();
    }

    public record struct FluentTypeConfiguration(SyntaxTree SyntaxTree, Type UnderlyingType, string Name);

    public class RuleBuilder<T> : FluentTypeBuilder<T>, IRuleBuilder<T>
    {
        private readonly FluentTypeConfiguration _type;

        public RuleBuilder(FluentTypeConfiguration type)
        {
            _type = type;
        }

        public IRuleBuilder<T> When(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> WithMessage(string message)
        {
            throw new NotImplementedException();
        }
    }

    public class FluentTypeBuilder<T> : IFluentTypeBuilder<T>
    {
        public IFluentTypeBuilder<T> AsClass()
        {
            throw new NotImplementedException();
        }

        public IFluentTypeBuilder<T> AsRecord()
        {
            throw new NotImplementedException();
        }

        public IFluentTypeBuilder<T> AsStruct()
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> ExclusiveBetween(T min, T max)
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> GreaterThan(T value)
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> GreaterThanOrEqual(T value)
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> InclusiveBetween(T min, T max)
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> Length(int min, T max)
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> Length(int exactLength)
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> LessThan(T value)
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> LessThanOrEqual(T value)
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> Matches(string regex)
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> MaxLength(int maxLength)
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> MinLength(int minLength)
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> Must(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IFluentTypeBuilder<T> Namespace(string value)
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> NotEmpty()
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> NotEqual(T value)
        {
            throw new NotImplementedException();
        }

        public IRuleBuilder<T> PrecisionScale(int precision, int scale)
        {
            throw new NotImplementedException();
        }

        public IFluentTypeBuilder<T> WithName(string message)
        {
            throw new NotImplementedException();
        }
    }
}
