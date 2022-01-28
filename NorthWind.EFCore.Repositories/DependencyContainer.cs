namespace NorthWind.Sales.UseCases;

public static class DependencyContainer
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
    {
        services.AddDbContext<NorthdWindSalesContext>(options=> options.UseSqlServer(configuration.GetConnectionString(connectionStringName)));
        services.AddScoped<INorthWindSalesCommandsReppository, NorthWindSalesCommandsRepository>();
        return services;
    }
}
