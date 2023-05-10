using AutoFixture;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Text.RegularExpressions;
using Typely.Core;

namespace Typely.Generators.Tests.Typely;

internal class TypelyGeneratorDriverFixture : BaseFixture<TypelyGeneratorDriver>
{
    private IEnumerable<SyntaxTree> _syntaxTrees = new List<SyntaxTree>();

    public TypelyGeneratorDriverFixture()
    {
        Fixture.Register(() => CreateCompilation(_syntaxTrees));
    }

    public TypelyGeneratorDriverFixture WithConfigurations(params Type[] configClasses)
    {
        _syntaxTrees = configClasses.Select(CreateSyntaxTree);
        return this;
    }

    public TypelyGeneratorDriverFixture WithNoConfiguration()
    {
        _syntaxTrees = new List<SyntaxTree> { CSharpSyntaxTree.ParseText("public class EmptyClass {}") };
        return this;
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
        if (!pathFromNamespace.Contains("/Typely/Configurations/"))
        {
            pathFromNamespace = $"Typely/Configurations/{pathFromNamespace}";
        }

        return  Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"../../../{pathFromNamespace}.cs");
    }

    private static Compilation CreateCompilation(IEnumerable<SyntaxTree> syntaxTrees) =>
        CSharpCompilation.Create(
            assemblyName: "tests",
            syntaxTrees: syntaxTrees,
            references: new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ITypelySpecification).Assembly.Location),
            });
}