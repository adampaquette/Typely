using Microsoft.AspNetCore.Mvc;
using Typely.AspNetCore.Mvc;
using Typely.AspNetCore.Mvc.ModelBinding;

namespace Typely.AspNetCore.Tests.Mvc;

public class MvcOptionsTests
{
    [Fact]
    public void UseTypelyModelBinderProvider_Should_InsertTypelyValueModelBinderProvider()
    {
        var options = new MvcOptions();
        options.UseTypelyModelBinderProvider();
        Assert.IsType<TypelyValueModelBinderProvider>(options.ModelBinderProviders[0]);
    }
}