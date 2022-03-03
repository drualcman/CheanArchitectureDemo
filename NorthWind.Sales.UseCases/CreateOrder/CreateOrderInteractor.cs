namespace NorthWind.Sales.UseCases.CreateOrder;

public class CreateOrderInteractor : ICreateOrderInputPort
{
    readonly INorthWindSalesCommandsReppository Repository;
    readonly ICreateOrderOutputPort OutputPort;
    readonly ValidationService<CreateOrderDto> Validator;
    readonly IDomainEventHub<SpecialOrderCreatedEvent> DomainEventHub;
    readonly ILogCommandsRepository LogCommandsRepository;
    readonly IDomainTransaction DomainTransaction;

    public CreateOrderInteractor(INorthWindSalesCommandsReppository repository, ICreateOrderOutputPort outputPort, ValidationService<CreateOrderDto> validator, IDomainEventHub<SpecialOrderCreatedEvent> domainEventHub, ILogCommandsRepository logCommandsRepository, IDomainTransaction domainTransaction)
    {
        Repository = repository;
        OutputPort = outputPort;
        Validator = validator;
        DomainEventHub = domainEventHub;
        LogCommandsRepository = logCommandsRepository;
        DomainTransaction = domainTransaction;
    }

    public async ValueTask Handle(CreateOrderDto orderDto)
    {
        await Validator.ExecuteValidadationsGuard(orderDto);

        LogCommandsRepository.Add(new DomainLog("Inicio de creacion de orde de compra"));
        await LogCommandsRepository.SaveChanges();

        OrderAggregate orderAggregate = OrderAggregate.From(orderDto);      //use a helper to create the OrderAgregate to follow single responsability

        try
        {
            //implement transaction, or all or nothing
            DomainTransaction.BeginTransaction();

            await Repository.CreateOrder(orderAggregate);
            await Repository.SaveChanges();

            LogCommandsRepository.Add(new DomainLog($"Order {orderAggregate.Id} creada."));
            await LogCommandsRepository.SaveChanges();
            await OutputPort.Handle(orderAggregate.Id);

            if (orderAggregate.OrderDetails.Count > 3)
            {
                await DomainEventHub.Raise(new SpecialOrderCreatedEvent(orderAggregate.Id, orderAggregate.OrderDetails.Count));
            }
            DomainTransaction.CommitTransaction();
        }
        catch
        {
            DomainTransaction.RollbackTransaction();
            string errorMessage = $"Creacion de orden {orderAggregate.Id} cancelada.";
            LogCommandsRepository.Add(new DomainLog(errorMessage));
            await LogCommandsRepository.SaveChanges();
            ApplicationStatusLoggerService.Log(new ApplicationStatusLog(errorMessage));
            throw;
        }
        
    }
}
