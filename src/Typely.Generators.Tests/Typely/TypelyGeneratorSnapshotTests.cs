using Typely.Generators.Tests.Typely.Configurations;

namespace Typely.Generators.Tests.Typely;

[UsesVerify]
public class TypelyGeneratorSnapshotTests
{
    [Theory]
    [InlineData(nameof(BasicConfiguration))]
    //[InlineData(nameof(NamespaceConfiguration))]
    public Task Basic(string className)
    {
        var driver = new TypelyGeneratorDriverFixture()
            .WithConfigurationFileFromClassName(className)
            .Create()
            .Run();

        return Verify(driver);
    }

    private SettingsTask Verify(object? target) => Verifier.Verify(target).UseDirectory("Snapshots");
}
