namespace NorthWind.Sales.IoC;

public static class DependencyContainer
{
    public static IServiceCollection AddNorthWindSalesServices (this IServiceCollection services, IConfiguration configuration, string connectionStringName)
    {
        services.AddRepositories(configuration, connectionStringName);
        services.AddUseCasesServices();
        services.AddPresenters();
        services.AddNorthWindSalesControllers();
        services.AddValidators();
        services.AddEntitiesServices();
        services.AddWebExceptionHandler();
        services.AddEventHandlers();
        services.AddMailService();
        return services;
    }
}
