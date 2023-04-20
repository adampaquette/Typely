using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Typely.AspNetCore.Tests.Mvc.ModelBinding;

public class TestModelBinderProviderContext : ModelBinderProviderContext
{
    public TestModelBinderProviderContext(ModelMetadata metadata)
    {
        Metadata = metadata;
    }

    public override BindingInfo? BindingInfo { get; }
    public override ModelMetadata Metadata { get; }
    public override IModelMetadataProvider? MetadataProvider { get; }

    public override IModelBinder CreateBinder(ModelMetadata metadata)
    {
        throw new NotImplementedException();
    }
}