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

    private SettingsTask Verify(object? target) => Verifier.Verify(target).UseDirectory("Snapshots");
}