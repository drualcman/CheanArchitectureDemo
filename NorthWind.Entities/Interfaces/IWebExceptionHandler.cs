namespace NorthWind.Entities.Interfaces;

public interface IWebExceptionHandler
{
    ValueTask<ProblemDetails> Handle(Exception ex, bool includeDetails);
}
