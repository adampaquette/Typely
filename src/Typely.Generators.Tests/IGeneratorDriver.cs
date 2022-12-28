using Microsoft.CodeAnalysis;

namespace Typely.Generators.Tests;
internal interface IGeneratorDriver
{
    GeneratorDriverRunResult Run();
}