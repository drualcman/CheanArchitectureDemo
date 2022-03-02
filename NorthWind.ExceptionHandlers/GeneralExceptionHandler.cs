namespace NorthWind.ExceptionHandlers;

public class GeneralExceptionHandler : IExceptionHandler<GeneralException>
{
    public ValueTask<ProblemDetails> Handle(GeneralException exception)
    {
        ApplicationStatusLoggerService.Log(new ApplicationStatusLog(LogLevel.Error, exception.Detail));
        return ValueTask.FromResult(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = StatusCodes.Status500InternalServerErrorType,
            Title = exception.Message,
            Detail = exception.Detail
        });
    }
}
