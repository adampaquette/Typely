using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Typely.Core;

namespace Typely.AspNetCore.Mvc.ModelBinding;

/// <summary>
/// Creates a binder for a Typely value.
/// </summary>
public class TypelyValueModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var implementedInterface = context.Metadata.ModelType.GetInterface(typeof(ITypelyValue<,>).FullName!);
        if (implementedInterface == null)
        {
            return null;
        }

        var binderType = typeof(TypelyValueModelBinder<,>).MakeGenericType(implementedInterface.GenericTypeArguments);
        return new BinderTypeModelBinder(binderType);
    }
}