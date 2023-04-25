using System.Text.Json;

namespace Typely.AspNetCore.Http.Tests;

public class TypelyValidationExceptionMiddlewareTests
{
    [Fact]
    public void Middleware_ShouldReturn_ErrorResponse()
    {
        var server = new TestServerFixture().WithTypelyMiddlewareAndValidationError().Create();
        var response = server.CreateClient().GetAsync("/").Result;
        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(response.Content.ReadAsStringAsync().Result);

        var attemptedValue = ((JsonElement)errorResponse!.ValidationError.AttemptedValue!).GetString();
        var placeholderValue =
            ((JsonElement)errorResponse!.ValidationError.PlaceholderValues!["Placeholder"]!).GetString();

        Assert.Equal(400, errorResponse!.StatusCode);
        Assert.Equal("ErrorCode", errorResponse.ValidationError.ErrorCode);
        Assert.Equal("ErrorMessageWithPlaceholders", errorResponse.ValidationError.ErrorMessage);
        Assert.Equal("AttemptedValue", attemptedValue);
        Assert.Equal("TypeName", errorResponse.ValidationError.TypeName);
        Assert.Equal("PlaceholderValue", placeholderValue);
    }
    
    [Fact]
    public void Middleware_Should_PassThrough()
    {
        var server = new TestServerFixture().WithTypelyMiddleware().Create();
        var response = server.CreateClient().GetAsync("/").Result;
       Assert.True(response.IsSuccessStatusCode);
    }
}