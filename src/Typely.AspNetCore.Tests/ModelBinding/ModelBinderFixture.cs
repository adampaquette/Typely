using Microsoft.AspNetCore.Mvc.ModelBinding;
using Typely.AspNetCore.ModelBinding;

namespace Typely.AspNetCore.Tests.ModelBinding;

public class ModelBinderFixture
{
    private Type _modelType = typeof(int);
    
    public ModelBinderFixture WithModelType(Type modelType)
    {
        _modelType = modelType;
        return this;
    }
    
    public IModelBinder? Build()
    {
        var binderProviderContext = new ModelBinderProviderContextFixture()
            .WithModelType(_modelType)
            .Build();

        return new TypelyValueModelBinderProvider().GetBinder(binderProviderContext);
    }
}