using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing.TypeBuilders;

internal class EmittableTypeBuilderBase
{
    protected readonly IEnumerable<ParsedInvocation> Invocations;
    protected readonly EmittableType EmittableType;
    private readonly SemanticModel _model;


    protected EmittableTypeBuilderBase(IEnumerable<ParsedInvocation> invocations, EmittableType emittableType,
        SemanticModel model)
    {
        Invocations = invocations;
        EmittableType = emittableType;
        _model = model;
    }

    protected bool TryHandleInvocation(ParsedInvocation invocation)
    {
        switch (invocation.MemberName)
        {
            case nameof(ITypelyBuilder<int>.For):
                EmittableType.SetTypeName(invocation.GetFirstStringArgument());
                return true;
            case nameof(ITypelyBuilder<int>.AsClass):
                EmittableType.AsClass();
                return true;
            case nameof(ITypelyBuilder<int>.AsStruct):
                EmittableType.AsStruct();
                return true;
            case nameof(ITypelyBuilder<int>.WithName):
                if (invocation.GetFirstExpressionArgument() is ParenthesizedLambdaExpressionSyntax lambdaExpression1)
                {
                    var expNamespace = GetContainingNamespace(lambdaExpression1);
                    EmittableType.AdditionalNamespaces.Add(expNamespace!);
                    EmittableType.SetName(lambdaExpression1.Body.ToString());
                }
                else
                {
                    EmittableType.SetName($"\"{invocation.GetFirstStringArgument()}\"");
                }

                return true;
            case nameof(ITypelyBuilder<int>.WithNamespace):
                EmittableType.SetNamespace(invocation.GetFirstStringArgument());
                return true;
            case nameof(IRuleBuilderOfInt.WithMessage):
                if (invocation.GetFirstExpressionArgument() is ParenthesizedLambdaExpressionSyntax lambdaExpression2)
                {
                    var expNamespace = GetContainingNamespace(lambdaExpression2);
                    EmittableType.AdditionalNamespaces.Add(expNamespace!);
                    EmittableType.SetCurrentMessage(lambdaExpression2.Body.ToString());
                }
                else
                {
                    EmittableType.SetCurrentMessage($"\"{invocation.GetFirstStringArgument()}\"");
                }

                return true;
            case nameof(IRuleBuilderOfInt.WithErrorCode):
                EmittableType.SetCurrentErrorCode(invocation.GetFirstStringArgument());
                return true;
            case nameof(IRuleBuilderOfInt.AsFactory):
                //TODO : Support as factory to reflet code
                return true;
        }

        return false;
    }

    protected string? GetContainingNamespace(ParenthesizedLambdaExpressionSyntax lambdaExpression) =>
        _model.GetSymbolInfo(lambdaExpression).Symbol?.ContainingNamespace.ToDisplayString();
    
    protected void AddRule(string errorCode, string rule,
        string message, params (string Key, object Value)[] placeholders) =>
        EmittableType.AddRule(EmittableRule.From(errorCode, rule, message, placeholders));
}