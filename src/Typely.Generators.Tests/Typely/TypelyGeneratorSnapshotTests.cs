using Typely.Generators.Tests.Typely.Configurations;
using Typely.Generators.Tests.Typely.Configurations.A;
using Typely.Generators.Tests.Typely.Configurations.B;

namespace Typely.Generators.Tests.Typely;

[UsesVerify]
public class TypelyGeneratorSnapshotTests
{
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
    public Task NoConfiguration_Should_NotThrow()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithNoConfiguration()
            .Create()
            .Run();

        return Verify(driver);
    }

    private SettingsTask Verify(object? target) => Verifier.Verify(target).UseDirectory("Snapshots");
}