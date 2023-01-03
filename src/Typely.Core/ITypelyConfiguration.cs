using Typely.Core.Builders;

namespace Typely.Core;

public interface ITypelyConfiguration
{
    void Configure(ITypelyBuilder builder);
}
