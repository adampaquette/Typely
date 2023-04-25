using Microsoft.AspNetCore.Builder;

namespace Typely.AspNetCore.Http;

/// <summary>
/// Application builder extensions.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds middleware that handle a <see cref="ValidationException"/> to return an <see cref="ErrorResponse"/> as JSON.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance this method extends.</param>
    /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseTypelyValidation(this IApplicationBuilder app) =>
        app.UseMiddleware<TypelyValidationExceptionMiddleware>();
}