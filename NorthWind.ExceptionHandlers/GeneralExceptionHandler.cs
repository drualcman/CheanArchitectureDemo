using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.ExceptionHandlers
{
    public class GeneralExceptionHandler : IExceptionHandler<GeneralException>
    {
        public ValueTask<ProblemDetails> Handle(GeneralException exception) =>
        ValueTask.FromResult(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = StatusCodes.Status500InternalServerErrorType,
            Title = exception.Message,
            Detail = exception.Detail
        });
    }
}
