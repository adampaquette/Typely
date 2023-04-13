using Typely.Core;

namespace Typely.Tests;

public class TypelyOptionsTests
{
    [Fact(Skip = "Impact other tests")]
    public void EnableSensitiveDataLogging_Should_OutputCurrentValue()
    {
        var expectedValue = 0;

        TypelyOptions.Instance.EnableSensitiveDataLogging();
        var validationError = TypelyOptionTestsType.Validate(expectedValue)!;
        TypelyOptions.Instance.EnableSensitiveDataLogging(false);

        Assert.Equal(expectedValue, validationError.PlaceholderValues[ValidationPlaceholders.Value]);
    }
}
