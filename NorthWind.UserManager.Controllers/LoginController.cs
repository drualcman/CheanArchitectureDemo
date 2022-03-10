namespace NorthWind.UserManager.Controllers;

public class LoginController : ILoginController
{
    readonly ILoginInputPort InputPort;
    readonly ILoginPresenter Presenter;

    public LoginController(ILoginInputPort inputPort, ILoginPresenter presenter)
    {
        InputPort = inputPort;
        Presenter = presenter;
    }

    public async ValueTask<string> Login(UserCredentialsDto userData)
    {
        await InputPort.Handle(userData);
        return Presenter.Token;
    }
}
