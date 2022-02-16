using NorthWind.Entities.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Entities.Interfaces
{
    public interface IExceptionHandler<ExceptionType> where ExceptionType : Exception
    {
        ValueTask<ProblemDetails> Handle(ExceptionType exception);
    }
}
