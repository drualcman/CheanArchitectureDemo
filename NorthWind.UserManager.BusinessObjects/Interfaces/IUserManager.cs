namespace NorthWind.UserManager.BusinessObjects.Interfaces;

public interface IUserManager
{
    Task<List<string>> Register(UserForRegistrationDto userData);
}
