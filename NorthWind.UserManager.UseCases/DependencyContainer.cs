namespace NorthWind.UserManager.UseCases;

public static class DependencyContainer
{
    public static IServiceCollection AddNorthWindUserManagerUseCasesServices(this IServiceCollection services)
    {
        services.AddScoped<IRegisterInputPort, RegisterInteractor>();
        return services;
    }
}
