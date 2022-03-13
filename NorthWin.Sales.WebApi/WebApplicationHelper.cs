using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace NorthWin.Sales.WebApi;

public static class WebApplicationHelper
{
    public static WebApplication CreateWebApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c => 
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Proporcionar el Token JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{ }
                }
            });
        });
        IConfigurationSection jwtConfigurationSection = builder.Configuration.GetSection("JWT");
        builder.Services.AddNorthWindSalesServices(builder.Configuration, "NothWindDb", "NotthWinDbUsers", jwtConfigurationSection);
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
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => 
            {
                jwtConfigurationSection.Bind(options.TokenValidationParameters);
                options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigurationSection["SecurityKey"]));
            });
        builder.Services.AddAuthorization();
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

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseNorthWindSalesEndPoints();
        app.UseCors();

        return app;
    }
}
