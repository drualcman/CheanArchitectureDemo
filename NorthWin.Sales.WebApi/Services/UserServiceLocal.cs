namespace NorthWin.Sales.WebApi.Services
{
    public class UserServiceLocal : IUserService
    {
        readonly IHttpContextAccessor Context;

        public UserServiceLocal(IHttpContextAccessor context)
        {
            Context = context;
        }

        public bool IsAuthenticaticed => Context.HttpContext.User.Identity.IsAuthenticated;

        public string UserName => Context.HttpContext.User.Identity?.Name;
    }
}
