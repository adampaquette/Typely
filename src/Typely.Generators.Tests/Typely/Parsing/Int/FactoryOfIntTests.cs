using Typely.Generators.Typely.Parsing;
using Typely.Generators.Typely.Parsing.Int;

namespace Typely.Generators.Tests.Typely.Parsing.String;

[UsesVerify]
public class FactoryOfIntTests
{
    [Fact]
    public void Type_Should_Match()
    {
        var expectedName = "Name";
        Factory.For(expectedName);

        Assert.Equal(expectedName, GetSingleEmittableType().TypeName);
    }

    [Fact]
    public void Namespace_Should_Match()
    {
        var expectedNamespace = "My";
        Factory.WithNamespace(expectedNamespace);

        Assert.Equal(expectedNamespace, GetSingleEmittableType().Namespace);
    }

    [Fact]
    public void ConstructTypeKind_ShouldBe_Struct()
    {
        Factory.AsStruct();

        Assert.Equal(ConstructTypeKind.Struct, GetSingleEmittableType().ConstructTypeKind);
    }

    [Fact]
    public void ConstructTypeKind_ShouldBe_Class()
    {
        Factory.AsClass();

        Assert.Equal(ConstructTypeKind.Class, GetSingleEmittableType().ConstructTypeKind);
    }

    [Fact]
    public void Name_Should_Match()
    {
        var expectedName = "Owner identifier";
        Factory.WithName(expectedName);

        Assert.Equal(expectedName, GetSingleEmittableType().Name!.Compile().Invoke());
    }

    [Fact]
    public void NameLambda_Should_Match()
    {
        var expectedName = "Owner identifier";
        Factory.WithName(() => expectedName);

        Assert.Equal(expectedName, GetSingleEmittableType().Name!.Compile().Invoke());
    }

    private FactoryOfInt Factory { get; } = (FactoryOfInt)new TypelyBuilderFixture().Create().OfInt().AsFactory();

    private EmittableType GetSingleEmittableType() => Assert.Single(Factory.GetEmittableTypes());
}