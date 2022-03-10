namespace NorthWind.UserManager.UseCases;

public static class DependencyContainer
{
    public static IServiceCollection AddNorthWindUserManagerUseCasesServices(this IServiceCollection services)
    {
        services.AddScoped<IRegisterInputPort, RegisterInteractor>();
        services.AddScoped<ILoginInputPort, LoginInteractor>();
        return services;
    }
}
