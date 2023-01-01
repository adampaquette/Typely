using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

public class TypelyBuilderTests
{
    [Fact]
    public void Type_Should_Match()
    {
        var expectedTypeName = "UserId";

        var builder = new TypelyBuilderFixture().Create();
        builder.For<int>(expectedTypeName);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        Assert.Equal(expectedTypeName, emittableType.TypeName);
    }

    [Fact]
    public void Namespace_Should_Match()
    {
        var expectedTypeName = "UserId";
        var expectedNamespace = "My";

        var builder = new TypelyBuilderFixture().Create();

        builder.For<int>(expectedTypeName)
            .Namespace(expectedNamespace);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        Assert.Equal(expectedNamespace, emittableType.Namespace);
    }

    [Fact]
    public void NotEmpty_ShouldBe_Set()
    {
        var expectedTypeName = "UserId";

        var builder = new TypelyBuilderFixture().Create();

        builder.For<int>(expectedTypeName)
            .NotEmpty();

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        var validation = Assert.Single(emittableType.Validations);
    }

    [Fact]
    public void ErrorCode_Should_Match()
    {
        var expectedTypeName = "UserId";
        var expectedErrorCode = "ERR001";

        var builder = new TypelyBuilderFixture().Create();

        builder.For<int>(expectedTypeName)
            .NotEmpty()
            .WithErrorCode(expectedErrorCode);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        var validation = Assert.Single(emittableType.Validations);
        Assert.Equal(expectedErrorCode, validation.ErrorCode);
    }

    [Fact]
    public void Message_Should_Match()
    {
        var expectedTypeName = "UserId";
        var expectedMessage = "Error message";

        var builder = new TypelyBuilderFixture().Create();

        builder.For<int>(expectedTypeName)
            .NotEmpty()
            .WithMessage(expectedMessage);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        var validation = Assert.Single(emittableType.Validations);
        Assert.Equal(expectedMessage, validation.ValidationMessage.Compile().Invoke());
    }

    [Fact]
    public void MessageLambda_Should_Match()
    {
        var expectedTypeName = "UserId";
        var expectedMessage = "Error message";

        var builder = new TypelyBuilderFixture().Create();

        builder.For<int>(expectedTypeName)
            .NotEmpty()
            .WithMessage(() => expectedMessage);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        var validation = Assert.Single(emittableType.Validations);
        Assert.Equal(expectedMessage, validation.ValidationMessage.Compile().Invoke());
    }

    [Fact]
    public void Name_Should_Match()
    {
        var expectedTypeName = "UserId";
        var expectedName = "Owner identifier";

        var builder = new TypelyBuilderFixture().Create();

        builder.For<int>(expectedTypeName)
            .Name(expectedName);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        Assert.Equal(expectedName, emittableType.Name.Compile().Invoke());
    }

    [Fact]
    public void NameLambda_Should_Match()
    {
        var expectedTypeName = "UserId";
        var expectedName = "Owner identifier";

        var builder = new TypelyBuilderFixture().Create();

        builder.For<int>(expectedTypeName)
            .Name(() => expectedName);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        Assert.Equal(expectedName, emittableType.Name.Compile().Invoke());
    }

    //[Fact]
    //public void ObjectType_ShouldBe_Class()
    //{
    //    var builder = new TypelyBuilderFixture().Create();

    //    builder.For<int>("UserId")
    //        .AsClass();

    //    var emittableTypes = builder.GetEmittableTypes();

    //    var emittableType = Assert.Single(emittableTypes);
    //    Assert.Equal(TypeKind.Class, emittableType.TypeKind);
    //}

    //[Fact]
    //public void ObjectType_ShouldBe_Struct()
    //{
    //    var builder = new TypelyBuilderFixture().Create();

    //    builder.For<int>("UserId")
    //        .AsStruct();

    //    var emittableTypes = builder.GetEmittableTypes();

    //    var emittableType = Assert.Single(emittableTypes);
    //    Assert.Equal(TypeKind.Struct, emittableType.TypeKind);
    //}

    //[Fact]
    //public void ObjectType_ShouldBe_Record()
    //{
    //    var builder = new TypelyBuilderFixture().Create();

    //    builder.For<int>("UserId")
    //        .AsRecord();

    //    var emittableTypes = builder.GetEmittableTypes();

    //    var emittableType = Assert.Single(emittableTypes);
    //    Assert.Equal(TypeKind.Record, emittableType.TypeKind);
    //}
}