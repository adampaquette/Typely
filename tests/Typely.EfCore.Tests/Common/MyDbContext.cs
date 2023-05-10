using Microsoft.EntityFrameworkCore;
using Typely.EfCore.Conventions;

namespace Typely.EfCore.Tests.Common;

public class MyDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; } = null!;

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var person = modelBuilder.Entity<Person>();
        
        person.Property(x => x.Id);
        person.Property(x => x.Email);
        person.Property(x => x.FirstName);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.AddTypelyConventions();
    }
}