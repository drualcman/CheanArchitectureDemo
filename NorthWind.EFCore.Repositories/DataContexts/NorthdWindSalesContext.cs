using System.Reflection;

namespace NorthWind.EFCore.Repositories.DataContexts;

public class NorthdWindSalesContext : DbContext
{
    public NorthdWindSalesContext(DbContextOptions<NorthdWindSalesContext> options) : base(options) { }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
