using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Typely.Generators.Typely;

namespace Typely.Generators.Tests.Typely;

internal class TypelyGeneratorDriver
{
    private readonly Compilation _compilation;

    public TypelyGeneratorDriver(Compilation compilation)
    {
        _compilation = compilation;
    }

    public GeneratorDriver Run()
    {
        // Directly create an instance of the generator
        // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
        var generator = new TypelyGenerator();
        // Create the driver that will control the generation, passing in our generator
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        // Run the generation pass
        // (Note: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls)
        return driver.RunGenerators(_compilation);
    }
}