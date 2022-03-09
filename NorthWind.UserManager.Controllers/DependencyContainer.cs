namespace NorthWind.UserManager.Controllers;

public static class DependencyContainer
{
    public static IServiceCollection AddNorthWindUserManagerControllers(this IServiceCollection services)
    {
        services.AddScoped<IRegisterController, RegisterController>();
        return services;
    }
}
