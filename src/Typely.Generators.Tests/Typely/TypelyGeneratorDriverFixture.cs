using AutoFixture;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Typely.Core;

namespace Typely.Generators.Tests.Typely;

internal class TypelyGeneratorDriverFixture : BaseFixture<TypelyGeneratorDriver>
{
    private Compilation? _compilation;

    public TypelyGeneratorDriverFixture()
    {
        Fixture.Register(() => _compilation);
    }

    public TypelyGeneratorDriverFixture WithConfigurationFileFromFileName(string fileName)
    {
        var sourceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"..\\..\\..\\{nameof(Typely)}\\{nameof(Configurations)}\\{fileName}");
        _compilation = CreateTypelyCompilation(sourceFilePath);
        return this;
    }

    public TypelyGeneratorDriverFixture WithClassConfigurationFile(string className)
    {
        var fileName = className + ".cs";
        return WithConfigurationFileFromFileName(fileName);
    }

    public TypelyGeneratorDriverFixture WithNoConfiguration()
    {
        _compilation = CSharpCompilation.Create(
            assemblyName: "tests",
            syntaxTrees: new[] { CSharpSyntaxTree.ParseText("public class EmptyClass {}") });
        return this;
    }

    private static Compilation CreateTypelyCompilation(string filePath)
    {
        var source = File.ReadAllText(filePath);

        return CSharpCompilation.Create(
            assemblyName: "tests",
            syntaxTrees: new[] { CSharpSyntaxTree.ParseText(source, path: filePath) },
            references: new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ITypelyConfiguration).Assembly.Location),
            });
    }
}