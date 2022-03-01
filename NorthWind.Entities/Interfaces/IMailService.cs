using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Entities.Interfaces
{
    /// <summary>
    /// interface to implement some mail service
    /// </summary>
    public interface IMailService
    {
        ValueTask SendMailToAdministrator(string subject, string body);
    }
}
