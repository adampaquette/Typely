using AutoFixture;
using AutoFixture.AutoMoq;

namespace Typely.Generators.Tests;

internal class BaseFixture<T>
{
    protected IFixture Fixture { get; }

    public BaseFixture()
    {
        Fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    public T Create() => Fixture.Create<T>();
}