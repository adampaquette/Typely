using Typely.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Typely.SourceGenerators.Extensions;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using Typely.Generators.Extensions;
using Basic.Reference.Assemblies;
using System.Linq.Expressions;

namespace Typely.Generators;

public partial class TypelyGenerator
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
        /// Filter classes having an interface name <see cref="ITypelysConfiguration"/>.
        /// </summary>
        internal static bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode) =>
            syntaxNode is ClassDeclarationSyntax c && c.HasInterface(nameof(ITypelyConfiguration));

        /// <summary>
        /// Filter classes having an interface <see cref="ITypelysConfiguration"/> that matches the 
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
            return classSymbol.AllInterfaces.Any(x => x.ToString() == typeof(ITypelyConfiguration).FullName) ?
                classDeclarationSyntax : null;
        }

        /// <summary>
        /// Execute the different <see cref="ITypelyConfiguration"/> classes founds and generate models of the desired user types.
        /// </summary>
        /// <param name="classes">Classes to parse.</param>
        /// <returns>A list of representation of desired user types.</returns>
        public IReadOnlyList<EmittableType> GetEmittableTypes(IEnumerable<ClassDeclarationSyntax> classes)
        {
            // We enumerate by syntax tree, to minimize impact on performance
            return classes.GroupBy(x => x.SyntaxTree).SelectMany(x => GetEmittableTypes(x.Key)).ToList().AsReadOnly();
        }

        /// <summary>
        /// Execute the different <see cref="ITypelyConfiguration"/> classes and generate models of the desired user types.
        /// </summary>
        /// <param name="syntaxTree">SyntaxTree to parse</param>
        /// <returns>A list of representation of desired user types.</returns>
        private IEnumerable<EmittableType> GetEmittableTypes(SyntaxTree syntaxTree)
        {
            // Stop if we're asked to
            _cancellationToken.ThrowIfCancellationRequested();

            var configurationAssembly = CreateConfigurationAssembly(syntaxTree);
            if (configurationAssembly == null)
            {
                return Array.Empty<EmittableType>();
            }

            var configurationTypes = configurationAssembly.GetTypes()
                .Where(x => x.GetInterfaces().Select(x => x.FullName).Contains(typeof(ITypelyConfiguration).FullName))
                .ToList();

            var emittableTypes = new List<EmittableType>();
            foreach (var configurationType in configurationTypes)
            {
                var configuration = (ITypelyConfiguration)configurationAssembly.CreateInstance(configurationType.FullName);
                var builder = new TypelyBuilder(syntaxTree, configurationType);
                configuration.Configure(builder);
                emittableTypes.AddRange(builder.GetEmittableTypes());
            }

            return emittableTypes;
        }

        /// <summary>
        /// Only resolve known assemblies. 
        /// </summary>
        private Assembly? CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) =>
            args.Name.Contains($"{nameof(Typely)}.{nameof(Core)}")
                ? AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName == args.Name)
                : null;        

        /// <summary>
        /// Compiles the user's code.
        /// </summary>
        private Assembly? CreateConfigurationAssembly(SyntaxTree syntaxTree)
        {
            var compilation = CSharpCompilation.Create(assemblyName: $"{nameof(Typely)}_{Path.GetRandomFileName()}")
                .WithReferenceAssemblies(ReferenceAssemblyKind.NetStandard20)
                .AddReferences(typeof(ITypelyConfiguration))
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

    public class TypelyBuilder : ITypelyBuilder
    {
        private List<EmittableType> _emittableTypes = new List<EmittableType>();
        private SyntaxTree _syntaxTree;
        private readonly Type _configurationType;

        public TypelyBuilder(SyntaxTree syntaxTree, Type configurationType)
        {
            _syntaxTree = syntaxTree;
            _configurationType = configurationType;
        }

        public ITypelyBuilder<T> For<T>(string typeName)
        {
            var emittableType = new EmittableType
            {
                UnderlyingType = typeof(T),
                SyntaxTree = _syntaxTree,
                Name = typeName,
                Namespace = _configurationType.Namespace
            };
            _emittableTypes.Add(emittableType);

            return new RuleBuilder<T>(emittableType);
        }

        public IReadOnlyList<EmittableType> GetEmittableTypes() =>
            _emittableTypes.ToList().AsReadOnly();
    }

    public record struct EmittableType(SyntaxTree SyntaxTree, Type UnderlyingType, string Name, string Namespace);

    public class RuleBuilder<T> : TypelyBuilder<T>, IRuleBuilder<T>
    {
        private readonly EmittableType _emittableType;

        public RuleBuilder(EmittableType emittableType)
        {
            _emittableType = emittableType;
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

    public class TypelyBuilder<T> : ITypelyBuilder<T>
    {
        public ITypelyBuilder<T> AsClass()
        {
            throw new NotImplementedException();
        }

        public ITypelyBuilder<T> AsRecord()
        {
            throw new NotImplementedException();
        }

        public ITypelyBuilder<T> AsStruct()
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

        public ITypelyBuilder<T> Namespace(string value)
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

        public ITypelyBuilder<T> WithName(string message)
        {
            throw new NotImplementedException();
        }
    }
}
