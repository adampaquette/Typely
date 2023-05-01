using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Typely.Core;

namespace Typely.AspNetCore.Http;

/// <summary>
/// Application builder extensions.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds a middleware to format Typely's <see cref="ValidationException"/> as JSON compatible with <see cref="HttpValidationProblemDetails"/>.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance this method extends.</param>
    /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseTypelyValidationResult(this IApplicationBuilder app) =>
        app.UseMiddleware<TypelyValidationResultMiddleware>();
}