using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Typely.Generators.Tests.Typely.Parsing;

public record ParserContext(ClassDeclarationSyntax ClassDeclarationSyntax, SemanticModel SemanticModel);