using Typely.Generators.Tests.Typely.Configurations;
using Typely.Generators.Tests.Typely.Configurations.A;
using Typely.Generators.Tests.Typely.Configurations.B;

namespace Typely.Generators.Tests.Typely;

[UsesVerify]
public class TypelyGeneratorSnapshotTests
{
    [Fact]
    public Task AllSupportedScenariosOfBool_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(BoolConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfByte_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(ByteConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfChar_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(CharConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfDateOnly_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(DateOnlyConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfDateTime_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(DateTimeConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfDateTimeOffset_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(DateTimeOffsetConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfInt_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(IntConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfDecimal_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(DecimalConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfFloat_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(FloatConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfGuid_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(GuidConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfDouble_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(DoubleConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfLong_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(LongConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfSByte_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(SByteConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfShort_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(ShortConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfString_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(StringConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfTimeOnly_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(TimeOnlyConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfTimeSpan_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(TimeSpanConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }
    
    [Fact]
    public Task AllSupportedScenariosOfUInt_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(UIntConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfULong_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(ULongConfiguration))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task AllSupportedScenariosOfUShort_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(UShortConfiguration))
            .Create()
            .Run();
        
        return Verify(driver);
    }

    [Fact]
    public Task MultipleConfigurations_Should_EmitTypes()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(MultipleConfigurationA), typeof(MultipleConfigurationB))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task EmptyConfiguration_Should_NotThrow()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(EmptyConfiguration))
            .Create()
            .Run();

        return Verify(driver);
    }
    
    [Fact]
    public Task NamespaceConfiguration_Should_NotThrow()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(NamespaceConfiguration))
            .Create()
            .Run();

        return Verify(driver);
    }
    
    [Fact]
    public Task WrappedNamespaceConfiguration_Should_NotThrow()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(ParentClass.WrappedNamespaceConfiguration))
            .Create()
            .Run();

        return Verify(driver);
    }
    
    [Fact]
    public Task NoNamespaceConfiguration_Should_NotThrow()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(NoNamespaceConfiguration))
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
    public Task MissingTypeName_Should_Continue()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurations(typeof(MissingTypeNameConfiguration))
            .Create()
            .Run();

        return Verify(driver);
    }
    
    private SettingsTask Verify(object? target) => Verifier.Verify(target).UseDirectory("Snapshots");
}