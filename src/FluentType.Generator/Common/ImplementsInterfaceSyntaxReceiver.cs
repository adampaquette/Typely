using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using FluentType.SourceGenerators.Extensions;
using FluentType.Core;

namespace FluentType.SourceGenerators.Commun;

internal class ImplementsInterfaceSyntaxReceiver<TInterface> : ISyntaxReceiver
{
    public IList<ClassDeclarationSyntax> Classes { get; } = new List<ClassDeclarationSyntax>();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax &&
            classDeclarationSyntax.HasInterface(nameof(IFluentTypesConfiguration)))
        {
            Classes.Add(classDeclarationSyntax);
        }
    }
}
