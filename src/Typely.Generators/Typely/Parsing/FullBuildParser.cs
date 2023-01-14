using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Typely.Core;
using Typely.Generators.Extensions;

namespace Typely.Generators.Typely.Parsing;

/// <summary>
/// Proof of concept to have fully compiled and fonctional ITypelyConfiguration.
/// </summary>
[ExcludeFromCodeCoverage]
internal sealed class FullBuildParser
{
    private readonly CancellationToken _cancellationToken;
    private readonly Compilation _compilation;
    private readonly Action<Diagnostic> _reportDiagnostic;

    public FullBuildParser(Compilation compilation, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
    {
        _compilation = compilation;
        _cancellationToken = cancellationToken;
        _reportDiagnostic = reportDiagnostic;
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

        return classSymbol.AllInterfaces.Any(x => x.ToString() == typeof(ITypelyConfiguration).FullName)
            ? classDeclarationSyntax
            : null;
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
    /// Compiles the user's code.
    /// </summary>
    private Assembly? CreateConfigurationAssembly(SyntaxTree syntaxTree)
    {
        return BuildConfigurationAssembly(syntaxTree);
    }

    /// <summary>
    /// Compiles the user's code.
    /// </summary>
    private Assembly? BuildConfigurationAssembly(SyntaxTree syntaxTree)
    {
        var projectDirectory = GetProjectDirectory(syntaxTree.FilePath);
        var outputDirectory = Path.Combine(projectDirectory.FullName, "bin\\typely-gen-cache\\");

        var projectFiles = projectDirectory.EnumerateFiles("*.csproj");
        if (!projectFiles.Any())
        {
            //diagnostic erreur
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            //Arguments = $"build {projectDirectory} --output {outputDirectory} --framework net7.0",
            Arguments = $"publish {projectDirectory} -property:OutDir={outputDirectory} -verbosity:normal",
            WindowStyle = ProcessWindowStyle.Hidden,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        var proc = Process.Start(startInfo);
        var publishOutput = proc.StandardOutput.ReadToEnd();
        proc.WaitForExit(5000);

        if (!IsSuccess(publishOutput))
        {
            //diagnostic erreur
        }

        var projectFileName = projectFiles.First().Name.Replace(".csproj", ".dll");
        var projectFilePath = Path.Combine(outputDirectory, projectFileName);
        if (!File.Exists(projectFilePath))
        {
            //diagnostic erreur
        }

        return Assembly.LoadFrom(projectFilePath);
    }

    private DirectoryInfo GetProjectDirectory(string path)
    {
        var folder = Path.GetDirectoryName(path);
        var directory = new DirectoryInfo(folder);

        while (!directory.EnumerateFiles("*.csproj").Any())
        {
            directory = directory.Parent;
        }

        return directory;
    }

    public bool IsSuccess(string buildOutput) => buildOutput.Contains("0 Error(s)") && buildOutput.Contains("Build succeeded.");

    private void Diag(DiagnosticDescriptor desc, Location? location, params object?[]? messageArgs)
    {
        _reportDiagnostic(Diagnostic.Create(desc, location, messageArgs));
    }
}