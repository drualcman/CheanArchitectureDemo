namespace NorthWind.Sales.UseCases.CreateOrder;

public class CreateOrderInteractor : ICreateOrderInputPort
{
    readonly INorthWindSalesCommandsReppository Repository;
    readonly ICreateOrderOutputPort OutputPort;
    readonly ValidationService<CreateOrderDto> Validator;
    readonly IDomainEventHub<SpecialOrderCreatedEvent> DomainEventHub;

    public CreateOrderInteractor(INorthWindSalesCommandsReppository repository, ICreateOrderOutputPort outputPort, ValidationService<CreateOrderDto> validator, IDomainEventHub<SpecialOrderCreatedEvent> domainEventHub)
    {
        Repository = repository;
        OutputPort = outputPort;
        Validator = validator;
        DomainEventHub = domainEventHub;
    }

    public async ValueTask Handle(CreateOrderDto orderDto)
    {
        await Validator.ExecuteValidadationsGuard(orderDto);        

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

        if (orderAggregate.OrderDetails.Count > 3)
        {
            await DomainEventHub.Raise(new SpecialOrderCreatedEvent(orderAggregate.Id, orderAggregate.OrderDetails.Count));
        }

        await OutputPort.Handle(orderAggregate.Id);
    }
}
