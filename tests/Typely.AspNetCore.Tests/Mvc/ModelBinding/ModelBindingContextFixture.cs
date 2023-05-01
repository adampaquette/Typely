using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Primitives;
using Moq;
using System.Globalization;

namespace Typely.AspNetCore.Tests.Mvc.ModelBinding;

public class ModelBindingContextFixture
{
    private Type? _modelType;
    private string? _value;
    
    public ModelBindingContextFixture WithModelType(Type modelType)
    {
        _modelType = modelType;
        return this;
    }
    
    public ModelBindingContextFixture WithValue(string? value)
    {
        _value = value;
        return this;
    }
    
    public ModelBindingContext Build()
    {
        var metadetailsProvider = new Mock<ICompositeMetadataDetailsProvider>();
        var modelMetadataProvider = new DefaultModelMetadataProvider(metadetailsProvider.Object);
        
        return new DefaultModelBindingContext
        {
            ModelMetadata = modelMetadataProvider.GetMetadataForType(_modelType!),
            ModelName = "SampleTypelyValue",
            ModelState = new ModelStateDictionary(),
            ValueProvider = new CompositeValueProvider
            {
                new QueryStringValueProvider(BindingSource.Query, new QueryCollection(
                        new Dictionary<string, StringValues>
                        {
                            { "SampleTypelyValue", new StringValues(_value) }
                        })
                , CultureInfo.InvariantCulture)
            },
            ActionContext = new ActionContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }
}