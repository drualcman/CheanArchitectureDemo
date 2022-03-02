namespace NorthWind.ExceptionHandlers;

public class WebExceptionHandler : IWebExceptionHandler
{
    static readonly Dictionary<Type, Type> ExceptionHandlers = new Dictionary<Type, Type>();

    public WebExceptionHandler(Assembly assembly)
    {
        Type[] types = assembly.GetTypes();
        foreach (Type t in types)
        {
            var handlers = t.GetInterfaces()
                .Where(i => i.IsGenericType
                         && i.GetGenericTypeDefinition() == typeof(IExceptionHandler<>));
            foreach (Type i in handlers)
            {
                var exceptionType = i.GetGenericArguments()[0];
                ExceptionHandlers.TryAdd(exceptionType, t);
            }
        }
    }

    public async ValueTask<ProblemDetails> Handle(Exception ex, bool includeDetails)
    {
        ProblemDetails problem;

        if (ExceptionHandlers.TryGetValue(ex.GetType(), out Type handlerType))
        {
            var handler = Activator.CreateInstance(handlerType);
            problem = await (ValueTask<ProblemDetails>)handlerType.GetMethod("Handle")
                .Invoke(handler, new object[] { ex })!;

            if (!includeDetails)
            {
                problem.Detail = "Consulte al administrador";
            }
        }
        else
        {
            // no encontro un manejador
            string title;
            string detail;
            if (includeDetails)
            {
                title = $"Un error ocurrio: {ex.Message}";
                detail = ex.ToString();
            }
            else
            {
                title = "Ha ocurrido un error al procesar la respuesta";
                detail = "Consulte al administrador.";
            }
            problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = StatusCodes.Status500InternalServerErrorType,
                Title = title,
                Detail = detail
            };
            ApplicationStatusLoggerService.Log(new ApplicationStatusLog(LogLevel.Error, ex.ToString()));
        }
        return problem;
    }
}
