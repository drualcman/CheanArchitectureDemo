namespace NorthWind.Sales.UseCases.CreateOrder;

public class CreateOrderInteractor : ICreateOrderInputPort
{
    readonly INorthWindSalesCommandsReppository Repository;
    readonly ICreateOrderOutputPort OutputPort;
    readonly ValidationService<CreateOrderDto> Validator;
    readonly IDomainEventHub<SpecialOrderCreatedEvent> DomainEventHub;
    readonly ILogCommandsRepository LogCommandsRepository;

    public CreateOrderInteractor(INorthWindSalesCommandsReppository repository, ICreateOrderOutputPort outputPort, ValidationService<CreateOrderDto> validator, IDomainEventHub<SpecialOrderCreatedEvent> domainEventHub, ILogCommandsRepository logCommandsRepository)
    {
        Repository = repository;
        OutputPort = outputPort;
        Validator = validator;
        DomainEventHub = domainEventHub;
        LogCommandsRepository = logCommandsRepository;
    }

    public async ValueTask Handle(CreateOrderDto orderDto)
    {
        await Validator.ExecuteValidadationsGuard(orderDto);

        LogCommandsRepository.Add(new DomainLog("Inicio de creacion de orde de compra"));
        await LogCommandsRepository.SaveChanges();

        OrderAggregate orderAggregate = OrderAggregate.From(orderDto);      //use a helper to create the OrderAgregate to follow single responsability
       
        await Repository.CreateOrder(orderAggregate);
        await Repository.SaveChanges();

        LogCommandsRepository.Add(new DomainLog($"Order {orderAggregate.Id} creada."));
        await LogCommandsRepository.SaveChanges();

        await OutputPort.Handle(orderAggregate.Id);

        if (orderAggregate.OrderDetails.Count > 3)
        {
            await DomainEventHub.Raise(new SpecialOrderCreatedEvent(orderAggregate.Id, orderAggregate.OrderDetails.Count));
        }
    }
}
