namespace NorthWind.Sales.BusinessObjects.Interfaces.Controllers;

public interface ICreateOrderController
{
    ValueTask<int> CreateOrder(CreateOrderDto orderDto);
}
