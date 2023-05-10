using Typely.Core;

namespace Typely.Tests;

public class TypelyOptionsTests
{
    [Fact]
    public void DisabledSensitiveDataLogging_ShouldNot_OutputCurrentValue()
    {
        TypelyOptions.Instance.EnableSensitiveDataLogging(false);
        var validationError = TypelyOptionTestsType.Validate(0)!;

        Assert.False( validationError.PlaceholderValues.ContainsKey(ValidationPlaceholders.Value));
    }
}
