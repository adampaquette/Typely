using System.Collections.Immutable;
using Typely.Core;
using Typely.Core.Builders;
using Typely.Generators.Typely.Emetting;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Typely.Parsing;

internal class EmittableTypeBuilderFactory
{
    public static IEmittableTypeBuilder Create(string defaultNamespace,
        ParsedExpressionStatement parsedExpressionStatement)
    {
        if (parsedExpressionStatement.Invocations.Count == 0)
        {
            throw new InvalidOperationException("MemberAccess cannot be empty");
        }

        var builderType = parsedExpressionStatement.Invocations.First().MemberName;
        var invocations = parsedExpressionStatement.Invocations.Skip(1);

        switch (builderType)
        {
            case nameof(ITypelyBuilder.OfString):
                return new EmittableTypeBuilderOfString(defaultNamespace, invocations);
            case nameof(ITypelyBuilder.OfInt):
                return new EmittableTypeBuilderOfInt(defaultNamespace, invocations);
            default: throw new InvalidOperationException($"Unknown builder type: {builderType}");
        }
    }
}

internal interface IEmittableTypeBuilder
{
    EmittableType Build();
}

internal class EmittableTypeBuilderBase
{
    protected readonly IEnumerable<ParsedInvocation> Invocations;
    protected readonly EmittableType EmittableType;

    protected EmittableTypeBuilderBase(IEnumerable<ParsedInvocation> invocations, EmittableType emittableType)
    {
        Invocations = invocations;
        EmittableType = emittableType;
    }

    public void SetBaseProperties()
    {
        foreach (var memberAccess in Invocations)
        {
            switch (memberAccess.MemberName)
            {
                case nameof(ITypelyBuilder<int>.For):
                    var typeName = memberAccess.ArgumentListSyntax.Arguments.First().ToString();
                    typeName = typeName.Substring(1, typeName.Length - 2);
                    EmittableType.SetTypeName(typeName);
                    break;
                case nameof(ITypelyBuilder<int>.AsClass):
                    EmittableType.AsClass();
                    break;
                case nameof(ITypelyBuilder<int>.AsStruct):
                    EmittableType.AsStruct();
                    break;
                case nameof(ITypelyBuilder<int>.WithName):
                    var name = memberAccess.ArgumentListSyntax.Arguments.First().ToString();
                    name = name.Substring(1, name.Length - 2);
                    EmittableType.SetName(name);
                    break;
                case nameof(ITypelyBuilder<int>.WithNamespace):
                    var @namespace = memberAccess.ArgumentListSyntax.Arguments.First().ToString();
                    @namespace = @namespace.Substring(1, @namespace.Length - 2);
                    EmittableType.SetNamespace(@namespace);
                    break;
            }
        }
    }
    
    protected void AddRule(string errorCode, string rule,
        string message, params (string Key, object Value)[] placeholders) =>
        EmittableType.AddRule(EmittableRule.From(errorCode, rule, message, placeholders));
}

internal class EmittableTypeBuilderOfInt : EmittableTypeBuilderBase, IEmittableTypeBuilder
{
    public EmittableTypeBuilderOfInt(string defaultNamespace, IEnumerable<ParsedInvocation> invocations)
        : base(invocations, new EmittableType(typeof(int), defaultNamespace))
    {
    }

    public EmittableType Build()
    {
        SetBaseProperties();
        
        foreach (var invocation in Invocations)
        {
            switch (invocation.MemberName)
            {
                case nameof(ITypelyBuilderOfInt.Must):
                    var must = invocation.ArgumentListSyntax.Arguments.First().ToString();
                    must = must.Substring(1, must.Length - 2);
                    AddRule(
                        errorCode: ErrorCodes.Must,
                        rule: $"!({must})",
                        message: nameof(ErrorMessages.Must));
                    break;
                case nameof(ITypelyBuilderOfInt.GreaterThan):
                    var value = invocation.ArgumentListSyntax.Arguments.First().ToString();
                    value = value.Substring(1, value.Length - 2);
                    AddRule(
                        errorCode: ErrorCodes.GreaterThan,
                        rule: $"{Emitter.ValueParameterName} <= {value}",
                        message: nameof(ErrorMessages.GreaterThan),
                        placeholders: (ValidationPlaceholders.ComparisonValue, value));
                    break;
                default: throw new NotSupportedException(invocation.MemberName);
            }
        }

        return EmittableType;
    }
}

internal class EmittableTypeBuilderOfString : EmittableTypeBuilderBase, IEmittableTypeBuilder
{
    public EmittableTypeBuilderOfString(string defaultNamespace, IEnumerable<ParsedInvocation> invocations)
        : base(invocations, new EmittableType(typeof(string), defaultNamespace))
    {
    }

    public EmittableType Build()
    {
        foreach (var invocation in Invocations)
        {
            switch (invocation.MemberName)
            {
                // case nameof(ITypelyBuilder<string>.For):
                //     var typeName = invocation.ArgumentListSyntax.Arguments.First().ToString();
                //     typeName = typeName.Substring(1, typeName.Length - 2);
                //     _builder.For(typeName);
                //     break;
                // case nameof(ITypelyBuilderOfString.Length):
                //     var length = int.Parse(invocation.ArgumentListSyntax.Arguments.First().ToString());
                //     _builder.Length(length);
                //     break;
                default: throw new NotSupportedException(invocation.MemberName);
            }
        }

        return EmittableType;
    }
}