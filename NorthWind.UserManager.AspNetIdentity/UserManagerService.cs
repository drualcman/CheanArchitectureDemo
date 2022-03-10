namespace NorthWind.UserManager.AspNetIdentity;

public class UserManagerService : IUserManager
{
    readonly UserManager<IdentityUser> UserManager;

    public UserManagerService(UserManager<IdentityUser> userManager)
    {
        UserManager = userManager;
    }

    public async Task<List<string>> Register(UserForRegistrationDto userData)
    {
        List<string> errors = null;

        IdentityUser user = new IdentityUser
        {
            UserName = userData.Email,
            Email = userData.Email
        };

        IdentityResult result = await UserManager.CreateAsync(user, userData.Password);

        if (!result.Succeeded)
        {
            errors = result.Errors.Select(e=> e.Description).ToList();
        }

        return errors;
    }

    public async Task<UserDto> GetUserByCredentials(UserCredentialsDto userData)
    {
        UserDto foundUser = default;

        IdentityUser user = await UserManager.FindByNameAsync(userData.UserName);
        if (user != null && await UserManager.CheckPasswordAsync(user, userData.Password))
        {
            foundUser = new UserDto(user.UserName, user.Email);
        }

        return foundUser;
    }
}
