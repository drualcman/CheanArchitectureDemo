using NorthWind.Entities.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Entities.Interfaces
{
    internal interface IWebExceptionHandler
    {
        ValueTask<ProblemDetails> Handle(Exception ex, bool includeDetails);
    }
}
