using Typely.Generators.Typely.Parsing;
using Typely.Generators.Typely.Parsing.Int;

namespace Typely.Generators.Tests.Typely.Parsing.String;

[UsesVerify]
public class TypelyBuilderOfIntTests
{
    [Fact] public Task NotEqual() => Builder.NotEqual(10).VerifyRules();

    [Fact] public Task NotEmpty() => Builder.NotEmpty().VerifyRules();

    [Fact] public Task LessThan() => Builder.LessThan(20).VerifyRules();

    [Fact] public Task LessThanOrEqualTo() => Builder.LessThanOrEqual(20).VerifyRules();

    [Fact] public Task GreaterThan() => Builder.GreaterThan(20).VerifyRules();

    [Fact] public Task GreaterThanOrEqualTo() => Builder.GreaterThanOrEqual(20).VerifyRules();

    [Fact] public Task Must() => Builder.Must((x) => x != 0).VerifyRules();

    [Fact] public void Type_Should_Match() => Assert.Equal("Name", GetSingleEmittableType().TypeName);

    [Fact]
    public void Namespace_Should_Match()
    {
        var expectedNamespace = "My";
        Builder.WithNamespace(expectedNamespace);

        Assert.Equal(expectedNamespace, GetSingleEmittableType().Namespace);
    }

    [Fact]
    public void ConstructTypeKind_ShouldBe_Struct()
    {
        Builder.AsStruct();

        Assert.Equal(ConstructTypeKind.Struct, GetSingleEmittableType().ConstructTypeKind);
    }

    [Fact]
    public void ConstructTypeKind_ShouldBe_Class()
    {
        Builder.AsClass();

        Assert.Equal(ConstructTypeKind.Class, GetSingleEmittableType().ConstructTypeKind);
    }

    [Fact]
    public void Name_Should_Match()
    {
        var expectedName = "Owner identifier";
        Builder.WithName(expectedName);

        Assert.Equal(expectedName, GetSingleEmittableType().Name!.Compile().Invoke());
    }

    [Fact]
    public void NameLambda_Should_Match()
    {
        var expectedName = "Owner identifier";
        Builder.WithName(() => expectedName);

        Assert.Equal(expectedName, GetSingleEmittableType().Name!.Compile().Invoke());
    }

    [Fact]
    public void ErrorCode_Should_Match()
    {
        var expectedErrorCode = "ERR001";
        Builder.NotEmpty().WithErrorCode(expectedErrorCode);

        Assert.Equal(expectedErrorCode, GetSingleEmittableRule().ErrorCode);
    }

    [Fact]
    public void Message_Should_Match()
    {
        var expectedMessage = "Error message";
        Builder.NotEmpty().WithMessage(expectedMessage);

        Assert.Equal(expectedMessage, GetSingleEmittableRule().Message.Compile().Invoke());
    }

    [Fact]
    public void MessageLambda_Should_Match()
    {
        var expectedMessage = "Error message";
        Builder.NotEmpty().WithMessage(() => expectedMessage);

        Assert.Equal(expectedMessage, GetSingleEmittableRule().Message.Compile().Invoke());
    }

    private RuleBuilderOfInt Builder { get; } = (RuleBuilderOfInt)new TypelyBuilderFixture().Create().OfInt().For("Name");

    private EmittableType GetSingleEmittableType() => Assert.Single(Builder.GetEmittableTypes());

    private EmittableRule GetSingleEmittableRule() => Assert.Single(GetSingleEmittableType().Rules);
}