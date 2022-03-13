using NorthWind.UserManager.BusinessObjects.DTOs;
using NorthWind.UserManager.BusinessObjects.Interfaces.Controllers;

namespace NorthWin.Sales.WebApi;

public static class EndPoints
{
    public static WebApplication UseNorthWindSalesEndPoints(this WebApplication app)
    {
        app.MapPost("/create", async (CreateOrderDto order, ICreateOrderController controller) => 
            Results.Ok(await controller.CreateOrder(order))).RequireAuthorization();
        app.MapPost("/user/register", async (UserForRegistrationDto user, IRegisterController controller) => {
            await controller.Register(user);
            return Results.Ok();
        });
        app.MapPost("/user/login", async (UserCredentialsDto user, ILoginController controller) => Results.Ok(await controller.Login(user)));
        return app;
    }
}
