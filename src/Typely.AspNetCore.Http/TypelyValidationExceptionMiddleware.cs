using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text.Json;
using Typely.Core;

namespace Typely.AspNetCore.Http;

/// <summary>
/// Middleware that handle a <see cref="ValidationException"/> to return an <see cref="ErrorResponse"/> as JSON.
/// </summary>
public class TypelyValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public TypelyValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, ValidationException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var errorResponse = new ErrorResponse
        {
            StatusCode = context.Response.StatusCode,
            ValidationError = exception.ValidationError,
            StackTrace = context.RequestServices.GetService<IHostEnvironment>()!.IsDevelopment()
                ? exception.StackTrace
                : null
        };

        var errorJson = JsonSerializer.Serialize(errorResponse);
        return context.Response.WriteAsync(errorJson);
    }
}