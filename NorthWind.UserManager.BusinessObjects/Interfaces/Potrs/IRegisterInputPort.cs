namespace NorthWind.UserManager.BusinessObjects.Interfaces.Potrs;

public interface IRegisterInputPort
{
    ValueTask Handle(UserForRegistrationDto userData);
}
