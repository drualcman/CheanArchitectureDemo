namespace NorthWind.Sales.UseCases.CreateOrder;

public class CreateOrderInteractor : ICreateOrderInputPort
{
    readonly INorthWindSalesCommandsReppository Repository;
    readonly ICreateOrderOutputPort OutputPort;
    readonly ValidationService<CreateOrderDto> Validator;
    readonly IDomainEventHub<SpecialOrderCreatedEvent> DomainEventHub;
    readonly ILogCommandsRepository LogCommandsRepository;
    readonly IDomainTransaction DomainTransaction;
    readonly IUserService UserService;

    public CreateOrderInteractor(INorthWindSalesCommandsReppository repository, ICreateOrderOutputPort outputPort, ValidationService<CreateOrderDto> validator, IDomainEventHub<SpecialOrderCreatedEvent> domainEventHub, ILogCommandsRepository logCommandsRepository, IDomainTransaction domainTransaction, IUserService userService)
    {
        Repository = repository;
        OutputPort = outputPort;
        Validator = validator;
        DomainEventHub = domainEventHub;
        LogCommandsRepository = logCommandsRepository;
        DomainTransaction = domainTransaction;
        UserService = userService;
    }

    public async ValueTask Handle(CreateOrderDto orderDto)
    {
        UserServiceGuards.CheckIfAuthorizedGuard(UserService);

        await Validator.ExecuteValidadationsGuard(orderDto);

        LogCommandsRepository.Add(new DomainLog("Inicio de creacion de orde de compra", UserService.UserName));
        await LogCommandsRepository.SaveChanges();

        OrderAggregate orderAggregate = OrderAggregate.From(orderDto);      //use a helper to create the OrderAgregate to follow single responsability

        try
        {
            //implement transaction, or all or nothing
            DomainTransaction.BeginTransaction();

            await Repository.CreateOrder(orderAggregate);
            await Repository.SaveChanges();

            LogCommandsRepository.Add(new DomainLog($"Order {orderAggregate.Id} creada.", UserService.UserName));
            await LogCommandsRepository.SaveChanges();
            await OutputPort.Handle(orderAggregate.Id);

            if (new SpecialOrderSpecification().IsSatisfiedBy(orderAggregate))
            {
                await DomainEventHub.Raise(new SpecialOrderCreatedEvent(orderAggregate.Id, orderAggregate.OrderDetails.Count));
            }
            DomainTransaction.CommitTransaction();
        }
        catch
        {
            DomainTransaction.RollbackTransaction();
            string errorMessage = $"Creacion de orden {orderAggregate.Id} cancelada.";
            LogCommandsRepository.Add(new DomainLog(errorMessage, UserService.UserName));
            await LogCommandsRepository.SaveChanges();
            ApplicationStatusLoggerService.Log(new ApplicationStatusLog(errorMessage));
            throw;
        }
        
    }

}
