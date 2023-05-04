using AutoFixture;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using Typely.Generators.Typely;

namespace Typely.Generators.Tests.Typely.Parsing;

internal class GeneratorSyntaxContextFixture : BaseFixture<GeneratorSyntaxContext>
{
    private IEnumerable<SyntaxTree> _syntaxTrees = new List<SyntaxTree>();

    public GeneratorSyntaxContextFixture()
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

            var constructorInfo = typeof(GeneratorSyntaxContext).GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new Type[] {typeof(SyntaxNode), typeof(Lazy<SemanticModel>), typeof(ISyntaxHelper) },
                null);

            GeneratorSyntaxContext generatorSyntaxContext = constructorInfo.Invoke(new object[] { /* constructor parameter values */ }) as GeneratorSyntaxContext;
            
            return new GeneratorSyntaxContext{ Node = { classSyntax, compilation.GetSemanticModel(classSyntax.SyntaxTree))};
        });
        Fixture.Register(() => CancellationToken.None);
    }

    public GeneratorSyntaxContextFixture WithConfigurations(params Type[] configClasses)
    {
        _syntaxTrees = configClasses.Select(CreateSyntaxTree);
        return this;
    }
    
    public GeneratorSyntaxContextFixture WithSyntaxTrees(params SyntaxTree[] syntaxTrees)
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