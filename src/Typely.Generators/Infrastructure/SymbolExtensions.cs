using Microsoft.CodeAnalysis;

namespace Typely.Generators.Infrastructure;

public static class SymbolExtensions
{
    public static bool Implements(this ITypeSymbol type, ITypeSymbol interfaceType)
    {
        foreach (var t in type.AllInterfaces)
        {
            if (SymbolEqualityComparer.Default.Equals(t, interfaceType))
            {
                return true;
            }
        }
        return false;
    }
}