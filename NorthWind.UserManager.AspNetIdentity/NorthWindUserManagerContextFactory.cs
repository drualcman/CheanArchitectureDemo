namespace NorthWind.UserManager.AspNetIdentity;

internal class NorthWindUserManagerContextFactory : IDesignTimeDbContextFactory<NorthWindUserManagerContext>
{
    /// <summary>
    /// Add-Migration InitialCreate -p NorthWind.UserManager.AspNetIdentity -s NorthWind.UserManager.AspNetIdentity 
    /// Update-Database -p NorthWind.UserManager.AspNetIdentity -s NorthWind.UserManager.AspNetIdentity
    /// </summary>
    public NorthWindUserManagerContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<NorthWindUserManagerContext>();
        optionBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=NotthWinDbUsers");
        return new NorthWindUserManagerContext(optionBuilder.Options);
    }
}
