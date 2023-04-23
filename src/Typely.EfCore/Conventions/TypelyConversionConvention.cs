using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Typely.Core;

namespace Typely.EfCore.Conventions;

/// <summary>
/// Apply the value converter <see cref="TypelyValueConverter{TTypelyValue, TValue}"/> as a convention 
/// to all <see cref="ITypelyValue{TValue,TTypelyValue}"/> properties when a model is being finalized.
/// </summary>
/// <remarks>
/// See <see href="https://aka.ms/efcore-docs-conventions">Model building conventions</see> for more information and examples.
/// </remarks>
public class TypelyConversionConvention : IModelFinalizingConvention
{
    /// <summary>
    /// Called when a model is being finalized.
    /// </summary>
    /// <param name="modelBuilder">The builder for the model.</param>
    /// <param name="context">Additional information associated with convention execution.</param>
    public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder,
        IConventionContext<IConventionModelBuilder> context)
    {
        var typelyType = typeof(ITypelyValue<,>);

        foreach (var typelyValueProperty in modelBuilder.GetTypelyValueProperties())
        {
            var underlyingValueType = typelyValueProperty.ClrType
                .GetInterfaces()
                .First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typelyType)
                .GetGenericArguments()[0];

            var converterType =
                typeof(TypelyValueConverter<,>).MakeGenericType(underlyingValueType, typelyValueProperty.ClrType);

            typelyValueProperty.Builder.HasConverter(converterType);
        }
    }
}