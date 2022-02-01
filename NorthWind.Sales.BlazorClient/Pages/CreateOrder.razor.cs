namespace NorthWind.Sales.BlazorClient.Pages;

public partial class CreateOrder
{
    ErrorBoundaryBase ErrorBoundary;

    CreateOrderDto Order = new CreateOrderDto()
    {
        OrderDetails = new List<CreateOrderDetailDto>()
    };

    void Recover() => 
        ErrorBoundary?.Recover();
}
