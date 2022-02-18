namespace NorthWind.Entities.Exceptions;

public class ProblemDetailsException : Exception 
{
    public ProblemDetails ProblemDetails { get; private set; }

    public ProblemDetailsException() { }

    public ProblemDetailsException(JsonElement jsonResponse)
    {
        ProblemDetails = JsonSerializer.Deserialize<ProblemDetails>(jsonResponse.GetRawText(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public ProblemDetailsException(ProblemDetails problemDetails)
    {
        ProblemDetails = problemDetails;
    }

    public override string Message => ProblemDetails.Title;

}
