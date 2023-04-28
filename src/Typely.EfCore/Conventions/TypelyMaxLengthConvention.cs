using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection;
using Typely.Core;
using Typely.Core.Extensions;

namespace Typely.EfCore.Conventions;

/// Configures the maximum length of data that can be stored in all <see cref="ITypelyValue{TValue,TTypelyValue}"/> 
/// properties when a model is being finalized.
/// </summary>
/// <remarks>
///   See <see href="https://aka.ms/efcore-docs-conventions">Model building conventions</see> for more information and examples.
/// </remarks>
public class TypelyMaxLengthConvention : IModelFinalizingConvention
{
    /// <summary>
    /// Called when a model is being finalized.
    /// </summary>
    /// <param name="modelBuilder">The builder for the model.</param>
    /// <param name="context">Additional information associated with convention execution.</param>
    public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder,
        IConventionContext<IConventionModelBuilder> context)
    {
        var maxLengthType = typeof(IMaxLength);

        foreach (var property in modelBuilder.GetTypelyValueProperties())
        {
            var typelyValueType = property.ClrType.GetTypeOrUnderlyingType();

            if (maxLengthType.IsAssignableFrom(typelyValueType))
            {
                var getMaxLengthMethod = typelyValueType.GetMethod("get_" + nameof(IMaxLength.MaxLength));
                var maxLength = (int)getMaxLengthMethod!.Invoke(null, null)!;

                property.Builder.HasMaxLength(maxLength);
            }

        }
    }
}