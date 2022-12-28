using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;
using System.Text;

namespace Typely.Generators.Logging;


internal static class Logger
{
#if DEBUG
    private static StringBuilder _logs { get; } = new();
#endif

    public static void Log(string message)
    {
#if DEBUG
        _logs.AppendLine(message);
#endif
    }

    public static void WriteFile(SourceProductionContext context)
    {
#if DEBUG
        context.AddSource($"logs.g.cs", SourceText.From("/*" + _logs.ToString() + "*/", Encoding.UTF8));
#endif
    }
}