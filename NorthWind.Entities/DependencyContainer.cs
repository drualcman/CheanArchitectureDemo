namespace NorthWind.Entities;

public static class DependencyContainer
{
    public static IServiceCollection AddEntitiesServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(ValidationService<>));
        return services;
    }
}
