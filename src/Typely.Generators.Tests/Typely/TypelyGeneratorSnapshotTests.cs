using Typely.Generators.Tests.Typely.Configurations;

namespace Typely.Generators.Tests.Typely;

[UsesVerify]
public class TypelyGeneratorSnapshotTests
{
    [Fact]
    public Task Basic()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithClassConfigurationFile(nameof(BasicConfiguration))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task Namespace()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithClassConfigurationFile(nameof(NamespaceConfiguration))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task NotEmpty()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithClassConfigurationFile(nameof(NotEmptyConfiguration))
            .Create()
            .Run();

        return Verify(driver);
    }

    [Fact]
    public Task Complete()
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithClassConfigurationFile(nameof(CompleteConfiguration))
            .Create()
            .Run();

        return Verify(driver);
    }

    private SettingsTask Verify(object? target) => Verifier.Verify(target).UseDirectory("Snapshots");
}
