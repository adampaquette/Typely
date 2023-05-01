using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using Typely.Core;

namespace Typely.AspNetCore.Mvc.ModelBinding;

/// <summary>
/// Binds a primitive to a <see cref="ITypelyValue{TValue, TTypelyValue}"/>.
/// </summary>
/// <typeparam name="TValue">A primitive value.</typeparam>
/// <typeparam name="TTypelyValue">A typely value object.</typeparam>
public class TypelyValueModelBinder<TValue, TTypelyValue> : IModelBinder
    where TTypelyValue : ITypelyValue<TValue, TTypelyValue>
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
            var converter = TypeDescriptor.GetConverter(valueType);
            var value = (TValue)converter.ConvertFromString(valueProviderResult.FirstValue!)!;

            if (TTypelyValue.TryFrom(value, out var typelyValue, out var validationError))
            {
                bindingContext.Result = ModelBindingResult.Success(typelyValue);
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, validationError!.ErrorMessage);
            }
        }
        catch (Exception ex)
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"The provided value could not be converted to the required type: {ex.Message}");
        }

        return Task.CompletedTask;
    }
}