using Buildalyzer;
using Typely.Core.Builders;

namespace Typely.POC;

public class Tests
{
    [Fact(Skip = "debug")]
    public void BadLanguageConstructs()
    {
        var a = new ReferenceSample();
        var c = default(ReferenceSample);
    }

    [Fact]
    public void BuildSolution()
    {
        var manager = new AnalyzerManager(@"C:\Users\nfs12\source\repos\Typely\src\Typely.sln");
        foreach(var project in manager.Projects)
        {
            var result = project.Value.Build();
        }
    }

    public ITypelyBuilder Builder()
    {
        throw new NotImplementedException();
    }
}