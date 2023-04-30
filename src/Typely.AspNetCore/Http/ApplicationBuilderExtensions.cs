using Microsoft.AspNetCore.Builder;
using Typely.Core;

namespace Typely.AspNetCore.Http;

/// <summary>
/// Application builder extensions.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds a middleware to format Typely's <see cref="ValidationException"/> as JSON.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance this method extends.</param>
    /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseTypelyValidationResult(this IApplicationBuilder app) =>
        app.UseMiddleware<TypelyValidationResultMiddleware>();
}