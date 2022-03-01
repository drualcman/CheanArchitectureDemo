namespace NorthWind.Mail;

public static class DependencyContainer
{
    public static IServiceCollection AddMailService(this IServiceCollection services)
    {
        services.AddSingleton<IMailService, MailService>();
        return services;
    }
}
