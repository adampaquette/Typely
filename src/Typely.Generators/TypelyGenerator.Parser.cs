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
        /// Execute the different <see cref="ITypelysConfiguration"/> classes founds and generate models of the desired user types.
        /// </summary>
        /// <param name="classes">Classes to parse.</param>
        /// <returns>A list of representation of desired user types.</returns>
        public IReadOnlyList<Typely> GetTypelys(IEnumerable<ClassDeclarationSyntax> classes)
        {
            // We enumerate by syntax tree, to minimize impact on performance
            return classes.GroupBy(x => x.SyntaxTree).SelectMany(x => GetTypelys(x.Key)).ToList().AsReadOnly();
        }

        /// <summary>
        /// Execute the different <see cref="ITypelysConfiguration"/> classes and generate models of the desired user types.
        /// </summary>
        /// <param name="syntaxTree">SyntaxTree to parse</param>
        /// <returns>A list of representation of desired user types.</returns>
        private IReadOnlyList<Typely> GetTypelys(SyntaxTree syntaxTree)
        {
            // Stop if we're asked to
            _cancellationToken.ThrowIfCancellationRequested();

            var configurationAssembly = CreateConfigurationAssembly(syntaxTree);
            if (configurationAssembly == null)
            {
                return Array.Empty<Typely>();
            }

            var configurationTypes = configurationAssembly.GetTypes()
                .Where(x => x.GetInterfaces().Select(x => x.FullName).Contains(typeof(ITypelyConfiguration).FullName))
                .ToList();

            var Typelys = new List<Typely>();
            foreach (var configurationType in configurationTypes)
            {
                dynamic TypelysConfiguration = configurationAssembly.CreateInstance(configurationType.FullName);
                var fluentBuilder = new TypelyBuilder(syntaxTree);
                TypelysConfiguration.Configure(fluentBuilder);
                Typelys.AddRange(fluentBuilder.GetTypelys());
            }

            return Typelys;
        }

        /// <summary>
        /// Only resolve known assemblies. 
        /// </summary>
        private Assembly? CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) =>
            args.Name == "Typely.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                ? AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName == args.Name)
                : null;        

        /// <summary>
        /// Compiles the user's code.
        /// </summary>
        private Assembly? CreateConfigurationAssembly(SyntaxTree syntaxTree)
        {
            var compilation = CSharpCompilation.Create(assemblyName: $"Typely_{Path.GetRandomFileName()}")
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
        private List<Typely> _Typelys = new List<Typely>();
        private SyntaxTree _syntaxTree;

        public TypelyBuilder(SyntaxTree syntaxTree)
        {
            _syntaxTree = syntaxTree;
        }

        public ITypelyBuilder<T> For<T>(string typeName)
        {
            var TypelyConfiguration = new Typely
            {
                UnderlyingType = typeof(T),
                SyntaxTree = _syntaxTree,
                Name = typeName
            };
            _Typelys.Add(TypelyConfiguration);

            return new RuleBuilder<T>(TypelyConfiguration);
        }

        public IReadOnlyList<Typely> GetTypelys() =>
            _Typelys.ToList().AsReadOnly();
    }

    public record struct Typely(SyntaxTree SyntaxTree, Type UnderlyingType, string Name);

    public class RuleBuilder<T> : TypelyBuilder<T>, IRuleBuilder<T>
    {
        private readonly Typely _type;

        public RuleBuilder(Typely type)
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
