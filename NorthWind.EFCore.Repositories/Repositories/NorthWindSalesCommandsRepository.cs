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

    public async ValueTask SaveChanges() =>
        await Context.SaveChangesAsync();
}
