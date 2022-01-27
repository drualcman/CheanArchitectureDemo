namespace NorthWind.Sales.Controllers;

public static class DependencyContainer
{
    public static IServiceCollection AddUseCasesServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateOrderController, CreateOrderController>();
        return services;
    }
}
