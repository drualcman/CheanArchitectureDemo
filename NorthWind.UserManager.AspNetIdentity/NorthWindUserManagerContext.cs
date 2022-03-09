namespace NorthWind.UserManager.AspNetIdentity;

public class NorthWindUserManagerContext : IdentityDbContext<IdentityUser>
{
    public NorthWindUserManagerContext(DbContextOptions options) : base(options) { }
}
