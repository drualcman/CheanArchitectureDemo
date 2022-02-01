namespace NorthWind.Sales.BlazorClient.Components;

public partial class CreateOrderComponent
{
    [Inject]
    public NorthWindSalesApiClient Client { get; set; }
        
    [Parameter]
    public CreateOrderDto Order { get; set; }

    string Message;

    void AddNewOrderDetailItem() => 
        Order.OrderDetails.Add(new CreateOrderDetailDto());

    async Task Send()
    {
        Message = "";
        int orderId = await Client.CreateOrderAsync(Order);
        Message = $"Orden {orderId} creada.";
    }
}
