namespace Typely.Tests;

[UsesVerify]
public class ValidationErrorTests
{
    [Fact]
    public Task ShouldContain_ExpectedValues() => Verify(ValidationErrorTestsType.Validate(""));
}
