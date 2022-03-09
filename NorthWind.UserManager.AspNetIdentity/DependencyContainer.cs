namespace NorthWind.UserManager.AspNetIdentity;

public static class DependencyContainer
{
    public static IServiceCollection AddNothWindUserManager(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
    {
        services.AddDbContext<NorthWindUserManagerContext>(options => options.UseSqlServer(configuration.GetConnectionString(connectionStringName)));
        services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<NorthWindUserManagerContext>();
        services.AddScoped<IUserManager, UserManagerService>();
        return services;
    }
}
