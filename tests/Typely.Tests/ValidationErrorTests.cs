using Typely.Core;

namespace Typely.Tests;

public class ValidationErrorTests
{
    [Fact]
    public Task Validate_ShouldReturn_ValidationError() => Verify(ValidationErrorTestsType.Validate(default));

    [Fact]
    public void Exception_ShouldBe_ValidationException()
    {
        try
        {
            ValidationErrorTestsType.From(default);
        }
        catch (ValidationException ex)
        {
            Assert.NotNull(ex.ValidationError);
        }
    }
}
