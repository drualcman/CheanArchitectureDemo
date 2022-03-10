namespace NorthWind.UserManager.UseCases.Login;

public class LoginInteractor : ILoginInputPort
{
    readonly IUserManager UserManager;
    readonly ILoginPresenter Presenter;

    public LoginInteractor(IUserManager userManager, ILoginPresenter presenter)
    {
        UserManager = userManager;
        Presenter = presenter;
    }

    public async ValueTask Handle(UserCredentialsDto userData)
    {
        UserDto user = await UserManager.GetUserByCredentials(userData);
        if (user == default)
        {
            throw new UnauthorizedAccessException("Las credenciales proporcionadas son incorrectas");
        }
        await Presenter.Handle(user);
    }
}
