using NorthWind.UserManager.BusinessObjects.DTOs;
using NorthWind.UserManager.BusinessObjects.Interfaces.Controllers;

namespace NorthWin.Sales.WebApi;

public static class EndPoints
{
    public static WebApplication UseNorthWindSalesEndPoints(this WebApplication app)
    {
        app.MapPost("/create", async (CreateOrderDto order, ICreateOrderController controller) => Results.Ok(await controller.CreateOrder(order)));
        app.MapPost("/user/register", async (UserForRegistrationDto user, IRegisterController controller) => {
            await controller.Register(user);
            return Results.Ok();
        });
        return app;
    }
}
