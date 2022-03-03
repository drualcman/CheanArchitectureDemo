using System.Reflection;

namespace NorthWind.EFCore.Repositories.DataContexts;

/// <summary>
/// Add-Migration InitialCreate -p NorthWind.EFCore.Repositories -s NorthWind.EFCore.Repositories -c NorthdWindContext
/// Update-Database -p NorthWind.EFCore.Repositories -s NorthWind.EFCore.Repositories -context NorthdWindContext
/// </summary>
internal class NorthWindContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=NotthWinDb01282022");
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<DomainLog> DomainLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
