namespace NorthWind.EFCore.Repositories.Repositories;

public class NorthWindSalesCommandsRepository : INorthWindSalesCommandsReppository
{
    readonly NorthdWindSalesContext Context;

    public NorthWindSalesCommandsRepository(NorthdWindSalesContext context)
    {
        Context = context;
    }

    public async ValueTask CreateOrder(OrderAggregate order)
    {
        await Context.AddAsync(order);
        foreach (var item in order.OrderDetails)
        {
            await Context.AddAsync(new OrderDetail
            {
                Order = order,
                ProductId = item.ProductId,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity
            });
        }
    }

    public decimal? GetCurrentBalance(string customerId)
    {
        var result = Context.Customers
            .Where(c=>c.Id == customerId)
            .Select(c=>c.CurrentBalance)
            .ToList();
        return result.Any() ? result[0] : null;
    }

    public Dictionary<int, int> GetUnitsInStockOf(List<int> productsIds)
    {
        return Context.Products
            .Where(p=> productsIds.Contains(p.Id))
            .ToDictionary(p=> p.Id, p=> p.UnitsInStock);
    }

    public async ValueTask SaveChanges()
    {
        try
        {
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new UpdateException(ex.InnerException?.Message ?? ex.Message, 
                ex.Entries.Select(e=> e.Entity.GetType().Name).ToList());
        }
        catch (Exception ex)
        {
            throw new GeneralException(ex.Message, ex);
        }
    }
}
