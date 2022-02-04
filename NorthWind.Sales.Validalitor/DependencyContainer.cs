namespace NorthWind.Sales.Validalitor;

public static class DependencyContainer
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<Entities.Interfaces.IValidator<CreateOrderDto>, CreateOrderDtoValidator>();
        services.AddScoped<Entities.Interfaces.IValidator<CreateOrderDto>, CreateOrderDtoPersisnceValidator>();
        
        //manual validation
        //services.AddScoped<Entities.Interfaces.IValidator<CreateOrderDto>, CreateOrderDroIfValidator>();
        return services;
    }
}
