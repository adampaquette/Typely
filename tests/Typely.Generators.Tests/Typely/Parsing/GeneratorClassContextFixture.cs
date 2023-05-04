using AutoFixture;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Typely.Generators.Typely;

namespace Typely.Generators.Tests.Typely.Parsing;

internal class GeneratorClassContextFixture : BaseFixture<GeneratorClassContext>
{
    private IEnumerable<SyntaxTree> _syntaxTrees = new List<SyntaxTree>();

    public GeneratorClassContextFixture()
    {
        Fixture.Register(() =>
        {
            var compilation = CreateCompilation(_syntaxTrees);
            var classSyntax = compilation.SyntaxTrees
                .First()
                .GetRoot()
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .First();

            return new GeneratorClassContext(classSyntax, compilation.GetSemanticModel(classSyntax.SyntaxTree));
        });
        Fixture.Register(() => CancellationToken.None);
    }

    public GeneratorClassContextFixture WithConfigurations(params Type[] configClasses)
    {
        _syntaxTrees = configClasses.Select(CreateSyntaxTree);
        return this;
    }
    
    public GeneratorClassContextFixture WithSyntaxTrees(params SyntaxTree[] syntaxTrees)
    {
        _syntaxTrees = syntaxTrees;
        return this;
    }

    public static SyntaxTree CreateSyntaxTree(Type configClass)
    {
        string sourceFilePath = GetFilePath(configClass);
        return CreateSyntaxTree(sourceFilePath);
    }

    private static SyntaxTree CreateSyntaxTree(string filePath)
    {
        var source = File.ReadAllText(filePath);
        return CSharpSyntaxTree.ParseText(source, path: filePath);
    }

    private static string GetFilePath(Type configClass)
    {
        var pathFromNamespace = configClass.FullName!.Replace("Typely.Generators.Tests", "").Replace(".", "/");
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"../../../{pathFromNamespace}.cs");
    }

    private static Compilation CreateCompilation(IEnumerable<SyntaxTree> syntaxTrees) =>
        CSharpCompilation.Create(
            assemblyName: "tests",
            syntaxTrees: syntaxTrees,
            references: new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            });
}