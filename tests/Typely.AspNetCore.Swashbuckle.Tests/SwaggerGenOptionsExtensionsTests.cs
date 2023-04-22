using Swashbuckle.AspNetCore.SwaggerGen;

namespace Typely.AspNetCore.Swashbuckle.Tests;

public class SwaggerGenOptionsExtensionsTests
{
    [Fact]
    public void UseTypelyValueSchemaFilter_Should_AddTheFilter()
    {
        var options = new SwaggerGenOptions();
        options.UseTypelyValueSchemaFilter();
        Assert.Single(options.SchemaFilterDescriptors);
    }
}