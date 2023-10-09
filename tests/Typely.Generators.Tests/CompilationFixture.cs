using AutoFixture;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Text.RegularExpressions;
using Typely.Core;

namespace Typely.Generators.Tests;

internal class CompilationFixture<TFixture> : CompilationFixture<TFixture, Compilation>
    where TFixture : CompilationFixture<TFixture>
{
}

internal class CompilationFixture<TFixture, T> : BaseFixture<T> where TFixture : CompilationFixture<TFixture, T>
{
    private IEnumerable<SyntaxTree> _syntaxTrees = new List<SyntaxTree>();

    public CompilationFixture()
    {
        Fixture.Register(CreateCompilation);
    }

    public TFixture WithSpecification<TSpecification>()
    {
        _syntaxTrees = new[] { CreateSyntaxTree(typeof(TSpecification)) };
        return (TFixture)this;
    }

    public TFixture WithSpecifications(params Type[] specClasses)
    {
        _syntaxTrees = specClasses.Select(CreateSyntaxTree);
        return (TFixture)this;
    }

    public TFixture WithoutSpecification()
    {
        _syntaxTrees = new List<SyntaxTree> { CSharpSyntaxTree.ParseText("public class EmptyClass {}") };
        return (TFixture)this;
    }

    private static SyntaxTree CreateSyntaxTree(string filePath)
    {
        var source = File.ReadAllText(filePath);
        return CSharpSyntaxTree.ParseText(source, path: filePath);
    }

    public static SyntaxTree CreateSyntaxTree(Type configClass)
    {
        string sourceFilePath = GetFilePath(configClass);
        return CreateSyntaxTree(sourceFilePath);
    }

    private static string GetFilePath(Type configClass)
    {
        var pathFromNamespace = configClass.FullName!.Replace("Typely.Generators.Tests", "").Replace(".", "/");

        //Remove nested class path
        pathFromNamespace = Regex.Replace(pathFromNamespace, @"(.+)\/(.+)\+(.+)", "$1/$3");

        //Add base path for class without namespace
        if (!pathFromNamespace.Contains("/Typely/Specifications/"))
        {
            pathFromNamespace = $"Typely/Specifications/{pathFromNamespace}";
        }

        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"../../../{pathFromNamespace}.cs");
    }

    protected Compilation CreateCompilation() =>
        CSharpCompilation.Create(
            assemblyName: "tests",
            syntaxTrees: _syntaxTrees,
            references: new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Linq.Expressions.Expression).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ITypelySpecification).Assembly.Location),
            });
}