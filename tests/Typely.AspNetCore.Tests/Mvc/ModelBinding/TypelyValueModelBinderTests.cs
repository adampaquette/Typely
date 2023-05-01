using Typely.AspNetCore.Mvc.ModelBinding;

namespace Typely.AspNetCore.Tests.Mvc.ModelBinding;

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
    public void BindModel_FillsStateError_WhenModelTypeCantBeConverted()
    {
        var modelType = typeof(Id);
        var binder = new ModelBinderFixture()
            .WithModelType(modelType)
            .Build();
        var bindingContext = new ModelBindingContextFixture()
            .WithModelType(modelType)
            .WithValue("j")
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
    
    [Fact]
    public void BindModel_ThrowsArgumentNullException_WhenBindingContextIsNull()
    {
        var binder = new TypelyValueModelBinder<string, Code>();
        Assert.ThrowsAsync<ArgumentNullException>(() => binder!.BindModelAsync(null!));
    }
    
    [Fact]
    public void BindModel_DoNothing_WhenValueProviderResultIsNone()
    {
        var modelType = typeof(Code);
        var binder = new ModelBinderFixture()
            .WithModelType(modelType)
            .Build();
        var bindingContext = new ModelBindingContextFixture()
            .WithModelType(modelType)
            .WithValue(null!)
            .Build();

        binder!.BindModelAsync(bindingContext);
        
        Assert.Null(bindingContext.Result.Model);
    }
}