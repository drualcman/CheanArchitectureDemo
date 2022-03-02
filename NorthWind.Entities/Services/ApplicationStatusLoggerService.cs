using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Entities.Services
{
    public class ApplicationStatusLoggerService
    {
        static ILogger<ApplicationStatusLoggerService> Logger;

        public static void SetLogger(ILogger<ApplicationStatusLoggerService> logger) => Logger = logger;

        public static void Log(ApplicationStatusLog log)
        {
            Logger?.Log(log.LogLevel, $"{log.CreatedDate} {log.Description}");

        }
    }
}
