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
    public static MvcOptions UseTypelyModelBinderProvider(this MvcOptions options)
    {
        options.ModelBinderProviders.Add(new TypelyValueModelBinderProvider());
        //options.ModelBinderProviders.Insert(0, new TypelyValueModelBinderProvider());
        return options;
    }
}