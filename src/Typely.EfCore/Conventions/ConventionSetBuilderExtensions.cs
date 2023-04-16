using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Typely.Core;

namespace Typely.EfCore.Conventions;

/// <summary>
/// Convention set builder extensions.
/// </summary>
public static class ConventionSetBuilderExtensions
{
    /// <summary>
    /// Apply base type conventions to all <see cref="ITypelyValue{TValue,TTypelyValue}"/> properties when a model is being finalized.
    /// </summary>
    /// <param name="conventionSetBuilder">Builder for configuring conventions.</param>
    /// <remarks>
    ///   Conventions applied are: 
    ///   <list type="bullet">
    ///     <item><description><see cref="TypelyConversionConvention"/></description></item>
    ///   </list>
    /// </remarks>
    /// <returns>The convention set builder</returns>
    public static ConventionSetBuilder AddTypelyConventions(this ConventionSetBuilder conventionSetBuilder) =>
        conventionSetBuilder
            .AddTypelyConversionConvention();

    /// <summary>
    /// Apply the value converter <see cref="TypelyValueConverter{TValue,TTypelyValue}"/> as a convention 
    /// to all <see cref="ITypelyValue{TValue,TTypelyValue}"/> properties when a model is being finalized.
    /// </summary>
    /// <param name="conventionSetBuilder">Builder for configuring conventions.</param>
    /// <returns>The convention set builder</returns>
    public static ConventionSetBuilder AddTypelyConversionConvention(this ConventionSetBuilder conventionSetBuilder)
    {
        conventionSetBuilder.Add(_ => new TypelyConversionConvention());
        return conventionSetBuilder;
    }
}