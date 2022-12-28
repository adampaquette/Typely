using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Typely.Generators.Tests.Typely;
public class TypelyGeneratorTests
{
    [Fact]
    public void Generator_With_SimpleConfiguration()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurationFileFromMethodName(nameof(Generator_With_SimpleConfiguration))
            .Create();

        var result = driver.Run();
        
        //Debug.Assert(result.GeneratedTrees.Length == 1);
        //Debug.Assert(result.Diagnostics.IsEmpty);
        //Assert.True(result.ex)

        //GeneratorRunResult generatorResult = result.Results[0];
        //Debug.Assert(generatorResult.Diagnostics.IsEmpty);
        //Debug.Assert(generatorResult.GeneratedSources.Length == 1);
        //Debug.Assert(generatorResult.Exception is null);
    }
}
