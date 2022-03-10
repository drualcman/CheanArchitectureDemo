using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.UserManager.BusinessObjects.Presenters
{
    public interface ILoginPresenter
    {
        string Token { get; }
        ValueTask Handle(UserDto userDto);
    }
}
