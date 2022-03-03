namespace NorthWind.Mail;

public class MailService : IMailService
{
    readonly IConfiguration Configuration;

    public MailService(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public async ValueTask SendMailToAdministrator(string subject, string body)
    {
        try
        {
            MailMessage message = new MailMessage(
                Configuration["MailService:From"],
                Configuration["MailService:AdministratorEMail"]
                );
            message.Subject = subject;
            message.Body = body;

            SmtpClient smtpClient = new SmtpClient(
                Configuration["MailService:Host"],
                int.Parse(Configuration["MailService:Port"]))
            {
                Credentials = new NetworkCredential(                    
                    Configuration["MailService:UserName"],
                    Configuration["MailService:Password"]),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(message);
        }
        catch (Exception ex)
        {
            //notificar no se pudo enviar el correo
            ApplicationStatusLoggerService.Log(new ApplicationStatusLog(LogLevel.Information, ex.Message));
            throw;
        }
    }
}
