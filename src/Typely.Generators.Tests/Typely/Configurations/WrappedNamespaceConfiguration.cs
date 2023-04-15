using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Configurations
{
    internal class ParentClass
    {
        internal class WrappedNamespaceConfiguration : ITypelyConfiguration
        {
            public void Configure(ITypelyBuilder builder)
            {
            }
        }
    }
}