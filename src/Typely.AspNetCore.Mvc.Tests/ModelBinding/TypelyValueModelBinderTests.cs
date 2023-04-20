namespace Typely.AspNetCore.Mvc.Tests.ModelBinding;

public class TypelyValueModelBinderTests
{
    [Fact]
    public void BindModel_FillsStateError_WhenModelTypeIsInvalid()
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
        
        Assert.False(bindingContext.ModelState.IsValid);
        Assert.NotEmpty(new[] { bindingContext.ModelState.Values.First().Errors.First() });
    }
    
    [Fact]
    public void BindModel_BindsValue_WhenModelTypeValid()
    {
        var modelType = typeof(Code);
        var binder = new ModelBinderFixture()
            .WithModelType(modelType)
            .Build();
        var bindingContext = new ModelBindingContextFixture()
            .WithModelType(modelType)
            .WithValue("ABCD")
            .Build();

        binder!.BindModelAsync(bindingContext);
        
        Assert.Equal(Code.From("ABCD"), bindingContext.Result.Model );
    }
}