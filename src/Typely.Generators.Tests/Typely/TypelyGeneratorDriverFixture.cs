using AutoFixture;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Typely.Core;

namespace Typely.Generators.Tests.Typely;

internal class TypelyGeneratorDriverFixture : BaseFixture<TypelyGeneratorDriver>
{
    private string? _sourceFilePath;

    public TypelyGeneratorDriverFixture()
    {
        Fixture.Register(() =>
        {
            if (_sourceFilePath == null)
            { 
                throw new ArgumentNullException(nameof(_sourceFilePath));
            }

            return CreateCompilation(_sourceFilePath);
        });
    }

    public TypelyGeneratorDriverFixture WithConfigurationFileFromFileName(string fileName)
    {
        _sourceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"..\\..\\..\\{nameof(Typely)}\\{nameof(Configurations)}\\{fileName}");
        return this;
    }

    public TypelyGeneratorDriverFixture WithClassConfigurationFile(string className)
    {
        var fileName = className + ".cs";
        return WithConfigurationFileFromFileName(fileName);
    }

    private static Compilation CreateCompilation(string filePath)
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
