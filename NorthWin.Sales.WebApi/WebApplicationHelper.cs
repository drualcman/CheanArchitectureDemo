namespace NorthWin.Sales.WebApi;

public static class WebApplicationHelper
{
    public static WebApplication CreateWebApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddNorthWindSalesServices(builder.Configuration, "NothWindDb", "NotthWinDbUsers");
        builder.Services.AddCors(options => 
        {
            options.AddDefaultPolicy(config => 
            {
                config.AllowAnyOrigin();
                config.AllowAnyMethod();
                config.AllowAnyHeader();
            });
        });

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSingleton<IUserService, UserService>();

        return builder.Build();
    }

    public static WebApplication ConfigureWebApplication(this WebApplication app)
    {
        ApplicationStatusLoggerService.SetLogger(app.Services.GetService<ILogger<ApplicationStatusLoggerService>>());
        app.UseExceptionHandler(builder => 
            builder.UseWebExceptionHandlerMiddleware(app.Environment,
                                                     app.Services.GetService<IWebExceptionHandler>()));
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseNorthWindSalesEndPoints();
        app.UseCors();

        return app;
    }
}
