using Typely.Generators.Tests.Typely.Configurations;

namespace Typely.Generators.Tests.Typely;

[UsesVerify]
public class TypelyGeneratorSnapshotTests
{
    [Fact]
    public Task Complete()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithClassConfigurationFile(nameof(CompleteConfiguration))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task UnsupportedConfiguration_Should_OutputErrors()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithClassConfigurationFile(nameof(UnsupportedConfiguration))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task EmptyConfiguration_Should_NotThrow()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithClassConfigurationFile(nameof(EmptyConfiguration))
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