using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Typely.AspNetCore.Swashbuckle;

/// <summary>
/// SwaggerGen options extensions.
/// </summary>
public static class SwaggerGenOptionsExtensions
{
    /// <summary>
    /// Adds the <see cref="TypelyValueSchemaFilter"/>.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <returns>The options.</returns>
    public static SwaggerGenOptions UseTypelyValueSchemaFilter(this SwaggerGenOptions options)
    {
        options.SchemaFilter<TypelyValueSchemaFilter>();
        return options;
    }
}
