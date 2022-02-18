namespace NorthWind.ExceptionHandlers
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddWebExceptionHandler(this IServiceCollection services,
            Assembly exceptinoAssembly)
        {
            services.AddSingleton<IWebExceptionHandler>(provider => new WebExceptionHandler(exceptinoAssembly));
            return services;
        }

        public static IServiceCollection AddWebExceptionHandler(this IServiceCollection services) =>
            AddWebExceptionHandler(services, Assembly.GetExecutingAssembly());

        public static IApplicationBuilder UseWebExceptionHandlerMiddleware(this IApplicationBuilder app,
            IHostEnvironment enviroment, IWebExceptionHandler handler)
        {
            app.Use((context, next) => WebExceptionHandlerMiddleware.WriteResponse(
                context, enviroment.IsDevelopment(), handler));
            return app;
        }

        //public static WebApplication UseExceptionHandlerMiddleware(this WebApplication app)
        //{
        //    return app;
        //}
    }
}