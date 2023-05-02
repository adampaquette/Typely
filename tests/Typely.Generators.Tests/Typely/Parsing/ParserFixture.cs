using AutoFixture;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Typely.Core;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

internal class ParserFixture : BaseFixture<Parser>
{
    private IEnumerable<SyntaxTree> _syntaxTrees = new List<SyntaxTree>();
    public Compilation Compilation { get; private set; } = null!;

    public ParserFixture()
    {
        Fixture.Register(() =>
        {
            Compilation = CreateCompilation(_syntaxTrees);
            return Compilation;
        });
        Fixture.Register(() => CancellationToken.None);
    }

    public ParserFixture WithConfigurations(params Type[] configClasses)
    {
        _syntaxTrees = configClasses.Select(CreateSyntaxTree);
        return this;
    }
    
    public ParserFixture WithSyntaxTrees(params SyntaxTree[] syntaxTrees)
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