namespace NorthWind.ExceptionHandlers;

public class UpdateExceptionHandler : IExceptionHandler<UpdateException>
{
    public ValueTask<ProblemDetails> Handle(UpdateException exception) =>
        ValueTask.FromResult(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = StatusCodes.Status400BadRequestType,
            Title = "Error de actualizacion.",
            Detail = exception.Message,
            InvalidParams = new Dictionary<string, List<string>> 
            {                 
                {
                    "Entries", exception.Entries.ToList()
                }
            }
        });
}
