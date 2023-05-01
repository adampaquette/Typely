using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Typely.AspNetCore.Tests.Mvc.ModelBinding;

public class TestModelBinderProviderContext : ModelBinderProviderContext
{
    public TestModelBinderProviderContext(ModelMetadata metadata, IModelMetadataProvider metadataProvider)
    {
        Metadata = metadata;
        MetadataProvider = metadataProvider;
    }

    public override BindingInfo BindingInfo { get; } = new();
    public override ModelMetadata Metadata { get; }
    public override IModelMetadataProvider MetadataProvider { get; }

    public override IModelBinder CreateBinder(ModelMetadata metadata)
    {
        throw new NotImplementedException();
    }
}