using Microsoft.EntityFrameworkCore;
using Sample.Domain.ContactAggregate;

namespace Sample.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Contact> Contacts => Set<Contact>();
}