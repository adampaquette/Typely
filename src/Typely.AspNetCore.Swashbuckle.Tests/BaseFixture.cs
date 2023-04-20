using AutoFixture;
using AutoFixture.AutoMoq;

namespace Typely.AspNetCore.Swashbuckle.Tests;

public class BaseFixture<T>
{
    protected IFixture Fixture { get; } = new Fixture().Customize(new AutoMoqCustomization());
    
    public T Create() => Fixture.Create<T>();
}