using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Typely.AspNetCore.Tests.ModelBinding;

public class TypelyValueModelBinderTests
{
    [Fact]
    public void GetBinder_ReturnsBind_WhenModelTypeIsTypelyValue()
    {
        var modelType = typeof(Code);
        var binder = new ModelBinderFixture()
            .WithModelType(modelType)
            .Build();
        var bindingContext = new ModelBindingContextFixture()
            .WithModelType(modelType)
            .WithValue("ABC")
            .Build();

        binder!.BindModelAsync(bindingContext);
        
        Assert.False(bindingContext.Result == ModelBindingResult.Failed());
    }
}