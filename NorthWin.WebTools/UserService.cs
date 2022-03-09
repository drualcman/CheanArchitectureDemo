namespace NorthWin.WebTools;

public class UserService : IUserService
{
    readonly IHttpContextAccessor Context;

    public UserService(IHttpContextAccessor context)
    {
        Context = context;
    }

    //public bool IsAuthenticaticed => Context.HttpContext.User.Identity.IsAuthenticated;

    //public string UserName => Context.HttpContext.User.Identity?.Name;      
    public bool IsAuthenticaticed => true;

    public string UserName => "user@northwind.com";
}
