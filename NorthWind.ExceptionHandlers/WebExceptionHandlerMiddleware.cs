namespace NorthWind.ExceptionHandlers;

public class WebExceptionHandlerMiddleware
{
    public static async Task WriteResponse(HttpContext context, bool includeDetails, IWebExceptionHandler handle)
    {
        IExceptionHandlerFeature exceptionDetail = context.Features.Get<IExceptionHandlerFeature>();
        Exception exception = exceptionDetail.Error;

        if (exception != null)
        {
            ProblemDetails problem = await handle.Handle(exception, includeDetails);
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problem.Status;
            var stream = context.Response.Body;
            await JsonSerializer.SerializeAsync(stream, problem);
        }
    }
}
