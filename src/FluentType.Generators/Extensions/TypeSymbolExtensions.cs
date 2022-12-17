using Microsoft.CodeAnalysis;

namespace FluentType.Generator.Extensions
{
    internal static class TypeSymbolExtensions
    {
        public static bool IsNullable(this ITypeSymbol typeSymbol) => typeSymbol.Name == "Nullable";
    }
}
