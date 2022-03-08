namespace NorthWind.Entities.Interfaces;

public interface IUserService
{
    bool IsAuthenticaticed { get; }
    string UserName { get; }
}
