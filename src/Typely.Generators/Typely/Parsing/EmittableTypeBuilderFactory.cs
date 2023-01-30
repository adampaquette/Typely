using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing;

internal class EmittableTypeBuilderFactory
{
    public static IEmittableTypeBuilder Create(string defaultNamespace, ParsedExpressionStatement invocationResult)
    {
        if (invocationResult.Invocations.Count == 0)
        {
            throw new InvalidOperationException("MemberAccess cannot be empty");
        }

        var builderType = invocationResult.Invocations.First().MemberName;
        var membersAccess = invocationResult.Invocations.Skip(1);
        var typelyBuilder = new TypelyBuilder(defaultNamespace);

        switch (builderType)
        {
            case nameof(ITypelyBuilder.OfString):
                return new EmittableTypeBuilderOfString(typelyBuilder, membersAccess);
            case nameof(ITypelyBuilder.OfInt):
                return new EmittableTypeBuilderOfInt(typelyBuilder, membersAccess);
            default: throw new InvalidOperationException($"Unknown builder type: {builderType}");
        }
    }
}

internal interface IEmittableTypeBuilder
{
    IReadOnlyList<EmittableType> Parse();
}

internal class EmittableTypeBuilderOfInt : IEmittableTypeBuilder
{
    private readonly TypelyBuilder _containerBuilder;
    private readonly ITypelyBuilderOfInt _builder;
    private IEnumerable<ParsedInvocation> _membersAccess;

    public EmittableTypeBuilderOfInt(TypelyBuilder containerBuilder, IEnumerable<ParsedInvocation> membersAccess)
    {
        _containerBuilder = containerBuilder;
        _builder = containerBuilder.OfInt();
        _membersAccess = membersAccess;
    }

    public IReadOnlyList<EmittableType> Parse()
    {
        foreach (var memberAccess in _membersAccess)
        {
            switch (memberAccess.MemberName)
            {
                case nameof(ITypelyBuilder<string>.For):
                    var typeName = memberAccess.ArgumentListSyntax.Arguments.First().ToString();
                    typeName = typeName.Substring(1, typeName.Length - 2);
                    _builder.For(typeName);
                    break;
                default: throw new NotSupportedException(memberAccess.MemberName);
            }
        }

        return _containerBuilder.GetEmittableTypes();
    }
}

internal class EmittableTypeBuilderOfString : IEmittableTypeBuilder
{
    private readonly TypelyBuilder _containerBuilder;
    private readonly ITypelyBuilderOfString _builder;
    private IEnumerable<ParsedInvocation> _membersAccess;

    public EmittableTypeBuilderOfString(TypelyBuilder containerBuilder, IEnumerable<ParsedInvocation> membersAccess)
    {
        _containerBuilder = containerBuilder;
        _builder = containerBuilder.OfString();
        _membersAccess = membersAccess;
    }

    public IReadOnlyList<EmittableType> Parse()
    {
        foreach (var memberAccess in _membersAccess)
        {
            switch (memberAccess.MemberName)
            {
                case nameof(ITypelyBuilder<string>.For):
                    var typeName = memberAccess.ArgumentListSyntax.Arguments.First().ToString();
                    typeName = typeName.Substring(1, typeName.Length - 2);
                    _builder.For(typeName);
                    break;
                case nameof(ITypelyBuilderOfString.Length):
                    var length = int.Parse(memberAccess.ArgumentListSyntax.Arguments.First().ToString());
                    _builder.Length(length);
                    break;
                default: throw new NotSupportedException(memberAccess.MemberName);
            }
        }

        return _containerBuilder.GetEmittableTypes();
    }
}