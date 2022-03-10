namespace NorthWind.UserManager.Presenter;

public static class DependencyContainer
{
    public static IServiceCollection AddNorthWindUserManagerPresenters(this IServiceCollection services, IConfigurationSection jwtConfigurationSection)
    {
        services.AddScoped<ILoginPresenter>(_ => new LoginPresenter(jwtConfigurationSection));
        return services;
    }
}
