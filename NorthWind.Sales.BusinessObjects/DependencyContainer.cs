namespace NorthWind.Sales.BusinessObjects;

public static class DependencyContainer
{
    public static IServiceCollection AddEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventHandler<SpecialOrderCreatedEvent>, SendEmailWhenSpecialOrderCreateEventHandler>();
        return services;
    }
}
