using Microsoft.AspNetCore.Mvc.ModelBinding;
using Typely.Core;

namespace Typely.AspNetCore.ModelBinding;

public class TypelyValueModelBinder<TValue, TTypelyValue> : IModelBinder where TTypelyValue : ITypelyValue<TValue, TTypelyValue>
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var valueType = typeof(TValue);
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

        try
        {
            var value = Convert.ChangeType(valueProviderResult.FirstValue, valueType);
            var typelyValueInstance = TypelyValue.From<TValue, TTypelyValue>((TValue)value);
            bindingContext.Result = ModelBindingResult.Success(typelyValueInstance);
        }
        catch (Exception ex)
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, ex.Message);
        }

        return Task.CompletedTask;
    }
}