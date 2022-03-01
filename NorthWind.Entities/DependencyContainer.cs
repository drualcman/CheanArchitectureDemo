namespace NorthWind.Entities;

public static class DependencyContainer
{
    public static IServiceCollection AddEntitiesServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(ValidationService<>));
        services.AddScoped(typeof(IDomainEventHub<>), typeof(DomainEventHub<>));
        return services;
    }
}
