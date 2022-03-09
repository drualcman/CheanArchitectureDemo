namespace NorthWind.UserManager.Controllers;

public class RegisterController : IRegisterController
{
    readonly IRegisterInputPort InputPort;

    public RegisterController(IRegisterInputPort inputPort)
    {
        InputPort = inputPort;
    }

    public ValueTask Register(UserForRegistrationDto userData)
    {
        return InputPort.Handle(userData);
    }
}
