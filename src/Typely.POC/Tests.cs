using Buildalyzer;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Diagnostics;
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
        foreach (var project in manager.Projects)
        {
            var result = project.Value.Build();
        }
    }

    [Fact]
    public void BuildSolution2()
    {
        var folder = "C:\\Users\\nfs12\\source\\repos\\Typely\\src\\Typely.Generators.Tests";
        var output = "C:\\Users\\nfs12\\source\\repos\\Typely\\src\\Typely.Generators.Tests\\bin\\typely-gen-cache\\";

        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            //Arguments = "build C:\\Users\\nfs12\\source\\repos\\Typely\\src\\Typely.Benchmarks\\ --output C:\\Users\\nfs12\\source\\repos\\Typely\\gen-cache",
            Arguments = $"build {folder} -c Debug --output {output} --framework net7.0",
            //Arguments = "build C:\\Users\\nfs12\\source\\repos\\Typely\\src\\Typely.Generators.Tests",
            WindowStyle = ProcessWindowStyle.Hidden,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        var proc = Process.Start(startInfo);
        var output2 = proc.StandardOutput.ReadToEnd();
        proc.WaitForExit(30000);
    }

    [Fact]
    public void BuildSolution3()
    {
        var folder = "C:\\Users\\nfs12\\source\\repos\\Typely\\src\\Typely.Generators.Tests";
        var output = "C:\\Users\\nfs12\\source\\repos\\Typely\\src\\Typely.Generators.Tests\\bin\\typely-gen-cache\\";

        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",            
            Arguments = $"publish {folder} -property:OutDir={output} -verbosity:normal",            
            WindowStyle = ProcessWindowStyle.Hidden,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        var proc = Process.Start(startInfo);
        var output2 = proc.StandardOutput.ReadToEnd();
        proc.WaitForExit(30000);
    }

    public ITypelyBuilder Builder()
    {
        throw new NotImplementedException();
    }
}