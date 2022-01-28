using Microsoft.Extensions.DependencyInjection;

namespace NorthWind.Sales.Presenters;

public static class DependencyContainer
{
    public static IServiceCollection AddPresenters(this IServiceCollection services)
    {
        services.AddScoped<CreateOrderPresenter>();
        services.AddScoped<ICreateOrderOutputPort>(p => p.GetService<CreateOrderPresenter>());
        services.AddScoped<ICreateOrderPresenter>(p => p.GetService<CreateOrderPresenter>());
        return services;
    }
}
