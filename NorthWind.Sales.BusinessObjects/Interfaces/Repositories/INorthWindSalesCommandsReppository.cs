namespace NorthWind.Sales.BusinessObjects.Interfaces.Repositories;

public interface INorthWindSalesCommandsReppository : IUnitOfWork
{
    ValueTask CreateOrder(OrderAggregate order);
}
