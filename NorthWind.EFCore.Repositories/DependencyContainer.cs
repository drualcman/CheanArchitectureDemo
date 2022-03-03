namespace NorthWind.EFCore.Repositories;

public static class DependencyContainer
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
    {
        services.AddDbContext<NorthWindSalesContext>(options => options.UseSqlServer(configuration.GetConnectionString(connectionStringName)));
        services.AddDbContext<NorthWindLogContext>(options => options.UseSqlServer(configuration.GetConnectionString(connectionStringName)));
        services.AddScoped<INorthWindSalesCommandsReppository, NorthWindSalesCommandsRepository>();
        services.AddScoped<ILogCommandsRepository, LogCommandsRepository>();
        return services;
    }
}
