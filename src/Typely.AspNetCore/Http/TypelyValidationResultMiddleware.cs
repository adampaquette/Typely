using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Typely.Core;

namespace Typely.AspNetCore.Http;

/// <summary>
/// Middleware that catches Typely's <see cref="ValidationException"/> and returns an <see cref="HttpValidationTemplatedProblemDetails"/> serialized.
/// </summary>
public class TypelyValidationResultMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JsonSerializerOptions _serializerOptions;

    public TypelyValidationResultMiddleware(RequestDelegate next)
    {
        _next = next;
        _serializerOptions =
            new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
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

    private Task HandleExceptionAsync(HttpContext context, ValidationException validationException)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var problemDetails = HttpValidationTemplatedProblemDetails.From(validationException);
        var errorJson = JsonSerializer.Serialize(problemDetails, _serializerOptions);
        return context.Response.WriteAsync(errorJson);
    }
}