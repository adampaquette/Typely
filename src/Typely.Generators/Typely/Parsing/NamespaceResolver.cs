using Microsoft.CodeAnalysis;

namespace Typely.Generators.Typely.Parsing;

public static class NamespaceResolver
{
    public static IEnumerable<string> GetNamespacesFromLambda(SyntaxNode lambdaNode, SemanticModel semanticModel)
    {
        var namespaces = new HashSet<string>();
        var symbolInfo = semanticModel.GetSymbolInfo(lambdaNode);

        if (symbolInfo.Symbol != null)
        {
            var containingNamespace = symbolInfo.Symbol.ContainingNamespace;
            if (containingNamespace != null)
            {
                namespaces.Add(containingNamespace.ToDisplayString());
            }
        }

        return namespaces;
    }
}