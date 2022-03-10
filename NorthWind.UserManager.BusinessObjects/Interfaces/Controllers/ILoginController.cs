using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.UserManager.BusinessObjects.Interfaces.Controllers
{
    public interface ILoginController
    {
        ValueTask<string> Login(UserCredentialsDto userData);
    }
}
