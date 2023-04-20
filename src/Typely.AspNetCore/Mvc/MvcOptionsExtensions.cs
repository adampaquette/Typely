using Microsoft.AspNetCore.Mvc;
using Typely.AspNetCore.Mvc.ModelBinding;

namespace Typely.AspNetCore.Mvc;

/// <summary>
/// MVC options extensions.
/// </summary>
public static class MvcOptionsExtensions
{
    /// <summary>
    /// Inserts the <see cref="TypelyValueModelBinderProvider"/>.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <returns>The options.</returns>
    public static MvcOptions UseTypelyModelBinders(this MvcOptions options)
    {
        options.ModelBinderProviders.Insert(0, new TypelyValueModelBinderProvider());
        return options;
    }
}