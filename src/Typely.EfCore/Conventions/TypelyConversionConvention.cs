﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Typely.Core;
using Typely.Core.Extensions;

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
        foreach (var property in modelBuilder.GetTypelyValueProperties())
        {
            var typelyValueType = property.ClrType.GetTypeOrUnderlyingType();

            var valueType = typelyValueType
                .GetInterfaces()
                .First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ITypelyValue<,>))
                .GetGenericArguments()[0];

            var converter = typeof(TypelyValueConverter<,>).MakeGenericType(valueType, typelyValueType);

            property.Builder.HasConverter(converter);
        }
    }
}