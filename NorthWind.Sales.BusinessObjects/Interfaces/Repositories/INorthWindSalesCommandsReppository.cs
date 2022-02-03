namespace NorthWind.Sales.BusinessObjects.Interfaces.Repositories;

public interface INorthWindSalesCommandsReppository : IUnitOfWork
{
    ValueTask CreateOrder(OrderAggregate order);
    decimal? GetCurrentBalance(string customerId);
    /// <summary>
    /// Return productId and quantity in stock
    /// </summary>
    /// <param name="productsIds">list of products to query</param>
    /// <returns></returns>
    Dictionary<int, int> GetUnitsInStockOf(List<int> productsIds);
}
