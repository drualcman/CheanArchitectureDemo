using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.UserManager.BusinessObjects.Interfaces.Potrs
{
    public interface ILoginInputPort
    {
        ValueTask Handle(UserCredentialsDto userData);
    }
}
