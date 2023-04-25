using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Typely.EfCore.Tests.Common;

namespace Typely.EfCore.Tests.Conventions;

public class ConventionTests
{
    [Fact]
    public void SaveChanges_ShouldWork_WithConventions()
    {
        using var serviceProvider = CreateServiceProvider();
        using var context = serviceProvider.GetRequiredService<MyDbContext>();

        var person = new Person
        {
            Id = PersonId.From(1), 
            Email = Email.From("a@a.com"), 
            FirstName = FirstName.From("John")
        };
        context.Persons.Add(person);
        context.SaveChanges();

        Assert.Equal(1, context.Persons.Count());
    }

    private static ServiceProvider CreateServiceProvider() => new ServiceCollection()
        .AddEntityFrameworkInMemoryDatabase()
        .AddDbContext<MyDbContext>(options => options.UseInMemoryDatabase("MyDb"))
        .BuildServiceProvider();
}