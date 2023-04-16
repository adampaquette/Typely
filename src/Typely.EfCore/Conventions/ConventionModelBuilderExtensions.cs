using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Typely.Core;
using Typely.Core.Extensions;

namespace Typely.EfCore.Conventions;

internal static class ConventionModelBuilderExtensions
{
    /// <summary>
    /// Gets all non-navigation properties implementing <see cref="ITypelyValue{TValue,TTypelyValue}"/> declared on the entity type.
    /// </summary>
    /// <param name="modelBuilder">Convention model builder</param>
    /// <returns>Declared non-navigation properties implementing <see cref="ITypelyValue{TValue,TTypelyValue}"/>.</returns>
    public static IEnumerable<IConventionProperty> GetBaseTypeConventionProperties(this IConventionModelBuilder modelBuilder) =>
        modelBuilder.Metadata
            .GetEntityTypes()
            .SelectMany(x => x.GetDeclaredProperties())
            .Where(x => x.ClrType.ImplementsITypelyValue());
}