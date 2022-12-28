using AutoFixture;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

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

            // Create the 'input' compilation that the generator will act on
            var source = File.ReadAllText(_sourceFilePath);
            return CreateCompilation(source);            
        });
    }

    public TypelyGeneratorDriverFixture WithConfigurationFileFromFileName(string fileName)
    {
        _sourceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"..\\..\\..\\Typely\\ConfigurationsUnderTest\\{fileName}");
        return this;
    }

    public TypelyGeneratorDriverFixture WithConfigurationFileFromMethodName(string methodName)
    {
        var prefixLength = "Generator_With_".Length;
        var fileName = methodName.Substring(prefixLength) + ".cs";
        return WithConfigurationFileFromFileName(fileName);
    }

    private static Compilation CreateCompilation(string source)
           => CSharpCompilation.Create("compilation",
               new[] { CSharpSyntaxTree.ParseText(source) },
               new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) },
               new CSharpCompilationOptions(OutputKind.ConsoleApplication));
}
