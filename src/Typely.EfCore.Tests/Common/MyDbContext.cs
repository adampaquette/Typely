using Microsoft.EntityFrameworkCore;
using Typely.EfCore.Conventions;

namespace Typely.EfCore.Tests.Common;

public class MyDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; } = null!;

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.AddTypelyConventions();
    }
}