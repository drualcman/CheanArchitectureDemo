namespace NorthWind.Entities.Guards;

public static class UserServiceGuards
{
    public static void CheckIfAuthorizedGuard(IUserService UserService)
    {
        if (!UserService.IsAuthenticaticed)
        {
            throw new UnauthorizedAccessException();
        }
    }
}
