using Typely.Generators.Tests.Typely.Specifications;
using Typely.Generators.Tests.Typely.Specifications.A;
using Typely.Generators.Tests.Typely.Specifications.B;

namespace Typely.Generators.Tests.Typely;

public class TypelyGeneratorSnapshotTests
{
    [Fact]
    public Task AllSupportedScenariosOfBool_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(BoolSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfByte_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(ByteSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfChar_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(CharSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfDateOnly_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(DateOnlySpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfDateTime_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(DateTimeSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfDateTimeOffset_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(DateTimeOffsetSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfInt_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(IntSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfDecimal_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(DecimalSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfFloat_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(FloatSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfGuid_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(GuidSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfDouble_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(DoubleSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfLong_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(LongSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfSByte_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(SByteSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfShort_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(ShortSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfString_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(StringSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfTimeOnly_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(TimeOnlySpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfTimeSpan_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(TimeSpanSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfUInt_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(UIntSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfULong_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(ULongSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfUShort_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(UShortSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task MultipleConfigurations_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(MultipleSpecificationA), typeof(MultipleSpecificationB))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task EmptyConfiguration_Should_NotThrow()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(EmptySpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task NamespaceConfiguration_Should_NotThrow()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(NamespaceSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task WrappedNamespaceConfiguration_Should_NotThrow()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(ParentClass.WrappedNamespaceSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task NoNamespaceConfiguration_Should_NotThrow()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(NoNamespaceSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task NoConfiguration_Should_NotThrow()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithNoConfiguration()
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task UnsupportedSpecification_Should_OutputDiagnostics()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(DiagnosticsSpecification))
            .Create()
            .Run();

        return Verify(driver);
    }

    private SettingsTask Verify(object? target) => Verifier.Verify(target).UseDirectory("Snapshots");
}