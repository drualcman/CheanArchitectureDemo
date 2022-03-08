namespace NorthWind.ExceptionHandlers;

public class UnauthorizedAccessExceptionHandler : IExceptionHandler<UnauthorizedAccessException>
{
    public ValueTask<ProblemDetails> Handle(UnauthorizedAccessException exception)
    {
        ApplicationStatusLoggerService.Log(new ApplicationStatusLog(LogLevel.Information, exception.Message));

        return ValueTask.FromResult(new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Type = StatusCodes.Status401UnauthorizedErrorType,
            Title = "Se requiere autenticacion de usuario para realizar la operacion."
        });
    }
}
