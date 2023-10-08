using System.Text.Json.Nodes;

namespace Typely.AspNetCore.Tests.Http;

[UsesVerify]
public class TypelyValidationResultMiddlewareTests
{
    [Fact]
    public async Task Middleware_ShouldReturn_ErrorResponse()
    {
        var server = new TestServerFixture().WithTypelyMiddlewareAndValidationError().Create();
        var response = await server.CreateClient().GetAsync("/");
        var jsonContent =  await response.Content.ReadAsStringAsync();
        var formattedJson = JsonNode.Parse(jsonContent)!.ToString();
        
        await Verify(formattedJson);
    }

    [Fact]
    public async void Middleware_Should_PassThrough()
    {
        var server = new TestServerFixture().WithTypelyMiddleware().Create();
        var response = await server.CreateClient().GetAsync("/");
        Assert.True(response.IsSuccessStatusCode);
    }
}