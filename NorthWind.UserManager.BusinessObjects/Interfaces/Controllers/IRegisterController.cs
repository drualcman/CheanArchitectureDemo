﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.UserManager.BusinessObjects.Interfaces.Controllers
{
    public interface IRegisterController
    {
        ValueTask Register(UserForRegistrationDto userData);
    }
}
