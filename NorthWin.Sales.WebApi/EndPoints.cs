namespace NorthWin.Sales.WebApi;

public static class EndPoints
{
    public static WebApplication UseNorthWindSalesEndPoints(this WebApplication app)
    {
        app.MapPost("/create", async (CreateOrderDto order, ICreateOrderController controller) => Results.Ok(await controller.CreateOrder(order)));
        return app;
    }
}
