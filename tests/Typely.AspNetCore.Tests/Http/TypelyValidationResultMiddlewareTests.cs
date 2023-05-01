using System.Text.Json.Nodes;

namespace Typely.AspNetCore.Tests.Http;

[UsesVerify]
public class TypelyValidationResultMiddlewareTests
{
    [Fact]
    public Task Middleware_ShouldReturn_ErrorResponse()
    {
        var server = new TestServerFixture().WithTypelyMiddlewareAndValidationError().Create();
        var response = server.CreateClient().GetAsync("/").Result;
        var jsonContent = response.Content.ReadAsStringAsync().Result;
        var formattedJson = JsonNode.Parse(jsonContent)!.ToString();
        
        return Verify(formattedJson);
    }

    [Fact]
    public void Middleware_Should_PassThrough()
    {
        var server = new TestServerFixture().WithTypelyMiddleware().Create();
        var response = server.CreateClient().GetAsync("/").Result;
        Assert.True(response.IsSuccessStatusCode);
    }
}