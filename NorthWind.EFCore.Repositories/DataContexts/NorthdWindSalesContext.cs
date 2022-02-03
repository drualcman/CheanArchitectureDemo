using System.Reflection;

namespace NorthWind.EFCore.Repositories.DataContexts;

public class NorthdWindSalesContext : DbContext
{
    public NorthdWindSalesContext(DbContextOptions<NorthdWindSalesContext> options) : base(options) { }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
