using Microsoft.EntityFrameworkCore;
namespace LazyLoading;

public class CommerceContext : DbContext
{
    public CommerceContext(DbContextOptions options) : base(options) { }

    public DbSet<Customer> Customers { get; set; } = null!; //null! = set it to null and disable the warning.
    public DbSet<Order> Orders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasKey(e => e.Id);

        modelBuilder.Entity<Order>().HasKey(e => e.Id);
    }
}
