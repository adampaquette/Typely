using Basic.Reference.Assemblies;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Reflection;
using Typely.Core;
using Typely.Generators.Extensions;

namespace Typely.Generators.Typely.Parsing;

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
    /// Filter classes having an interface name <see cref="ITypelyConfiguration"/>.
    /// </summary>
    internal static bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode) =>
        syntaxNode is ClassDeclarationSyntax c && c.HasInterface(nameof(ITypelyConfiguration));

    /// <summary>
    /// Filter classes having an interface <see cref="ITypelyConfiguration"/> that matches the 
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
            .AddReferences(NetStandard20.References.All)
            .AddReferences(typeof(ITypelyConfiguration))
            //.AddReferences(_compilation.References) //Swap with thoses for optimisation
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

