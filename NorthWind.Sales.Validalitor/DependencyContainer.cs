namespace NorthWind.Sales.Validalitor;

public static class DependencyContainer
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<Entities.Interfaces.IValidator<CreateOrderDto>, CreateOrderDtoValidator>();
        services.AddScoped<Entities.Interfaces.IValidator<CreateOrderDetailDto>, CreateOrderDetailDtoValidator>();
        return services;
    }
}
