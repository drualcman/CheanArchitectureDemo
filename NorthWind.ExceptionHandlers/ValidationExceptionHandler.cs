namespace NorthWind.ExceptionHandlers;

public class ValidationExceptionHandler : IExceptionHandler<ValidationException>
{
    public ValueTask<ProblemDetails> Handle(ValidationException exception)
    {
        ApplicationStatusLoggerService.Log(new ApplicationStatusLog(LogLevel.Error, exception.Message));

        return ValueTask.FromResult(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = StatusCodes.Status400BadRequestType,
            Title = "Error en los datos de entrada.",
            Detail = "Se eonctraron uno o mas errores de validacion de datos.",
            InvalidParams = exception.Failures
        });
    }
}
