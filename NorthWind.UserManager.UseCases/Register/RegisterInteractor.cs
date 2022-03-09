namespace NorthWind.UserManager.UseCases.Register;

public class RegisterInteractor : IRegisterInputPort
{
    readonly IUserManager UserManager;

    public RegisterInteractor(IUserManager userManager)
    {
        UserManager = userManager;
    }

    public async ValueTask Handle(UserForRegistrationDto userData)
    {
        List<string> errors = await UserManager.Register(userData);
        if (errors != null && errors.Any())
        {
            throw new ValidationException("No es posible realizar el registro", errors);
        }
    }
}
