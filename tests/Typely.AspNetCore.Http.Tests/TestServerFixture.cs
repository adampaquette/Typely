using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using Typely.Core;

namespace Typely.AspNetCore.Http.Tests;

public class TestServerFixture
{
    private IWebHostBuilder _webHostBuilder = new WebHostBuilder();

    public TestServerFixture WithTypelyMiddlewareAndValidationError()
    {
        _webHostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseTypelyValidation();
                app.Run(context =>
                {
                    throw new ValidationException(
                        new ValidationError(
                            "ErrorCode",
                            "ErrorMessageWithPlaceholders",
                            "AttemptedValue",
                            "TypeName",
                            new Dictionary<string, object?> { { "Placeholder", "PlaceholderValue" } }));
                });
            });

        return this;
    }
    
    public TestServerFixture WithTypelyMiddleware()
    {
        _webHostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseTypelyValidation();
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

        return this;
    }
    
    public TestServer Create() => new(_webHostBuilder);
}