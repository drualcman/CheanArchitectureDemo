namespace NorthWind.Sales.UseCases.CreateOrder;

public class CreateOrderInteractor : ICreateOrderInputPort
{
    readonly INorthWindSalesCommandsReppository Repository;
    readonly ICreateOrderOutputPort OutputPort;

    public CreateOrderInteractor(INorthWindSalesCommandsReppository repository, ICreateOrderOutputPort outputPort)
    {
        Repository = repository;
        OutputPort = outputPort;
    }

    public async ValueTask Handle(CreateOrderDto orderDto)
    {
        OrderAggregate orderAggregate = new OrderAggregate
        {
            CustomerId = orderDto.CustomerId,
            ShipAddress = orderDto.ShipAddress,
            ShipCity = orderDto.ShipCity,
            ShipCountry = orderDto.ShipCountry,
            ShipPostalCode = orderDto.ShipPostalCode
        };
        foreach (CreateOrderDetailDto item in orderDto.OrderDetails)
        {
            orderAggregate.AddDetail(item.ProductId, item.UnitPrice, item.Quantity);
        }
        await Repository.CreateOrder(orderAggregate);
        await Repository.SaveChanges();
        await OutputPort.Handle(orderAggregate.Id);
    }
}
