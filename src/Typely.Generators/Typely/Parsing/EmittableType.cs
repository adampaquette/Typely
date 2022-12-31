using Microsoft.CodeAnalysis;
using System;
using System.Linq.Expressions;

namespace Typely.Generators.Typely.Parsing;

internal class EmittableType
{
    public SyntaxTree? SyntaxTree { get; set; }
    public Type? UnderlyingType { get; set; }
    public string? TypeName { get; set; }
    public string? Namespace { get; set; }
    public TypeKind TypeKind { get; set; } = TypeKind.Struct;
    public List<EmittableValidation> Validations { get; set; } = new List<EmittableValidation>();
    public EmittableValidation? CurrentValidation { get; set; }
}

internal class EmittableValidation
{
    public Expression? ValidationExpression { get; set; }
    public string? ValidationMessage { get; set; }
}