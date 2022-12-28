using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;
using System.Text;

namespace Typely.Generators.Logging;


internal static class Logger
{
    private static StringBuilder _logs { get; } = new();

    public static void Log(string message)
    {
        _logs.AppendLine(message);
    }

    public static void WriteFile(SourceProductionContext context)
    {
        context.AddSource($"Logs.g.cs", SourceText.From("/*" + _logs.ToString() + "*/", Encoding.UTF8));
        _logs.Clear();
    }
}