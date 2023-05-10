using Typely.Core;

namespace Typely.Isolated.Tests;

public class ValidationErrorFactoryTests
{
    [Fact]
    public void EnableSensitiveDataLogging_Should_AddValueToPlaceholders()
    {
        TypelyOptions.Instance.EnableSensitiveDataLogging();
        var validationError = ValidationErrorFactory.Create("value123", "code", "message", "typeName", null);
        TypelyOptions.Instance.EnableSensitiveDataLogging(false);
        
        Assert.Equal("value123", validationError.PlaceholderValues[ValidationPlaceholders.Value]);
    }
}