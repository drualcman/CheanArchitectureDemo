namespace NorthWind.Sales.BusinessObjects.Aggregates;

public class OrderAggregate : Order
{
    readonly List<OrderDetail> OrderDetailsField = new List<OrderDetail>();
    public IReadOnlyCollection<OrderDetail> OrderDetails => OrderDetailsField;

    public void AddDetail(OrderDetail orderDetail)
    {
        OrderDetail existingOrderDetail = OrderDetailsField.FirstOrDefault(o => o.ProductId == orderDetail.ProductId);

        if (existingOrderDetail != default)
        {
            OrderDetailsField.Add(existingOrderDetail with
            {
                Quantity = (short)(existingOrderDetail.Quantity + orderDetail.Quantity),
            });
            OrderDetailsField.Remove(existingOrderDetail);
        }
        else OrderDetailsField.Add(orderDetail);
    }

    public void AddDetail(int productId, decimal unitPrice, short quantity) =>
        AddDetail(new OrderDetail(productId, unitPrice, quantity));

    /// <summary>
    /// Follow single responsability to create a OrderAggregate
    /// </summary>
    /// <param name="orderDto"></param>
    /// <returns></returns>
    public static OrderAggregate From(CreateOrderDto orderDto)
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
        return orderAggregate;
    }
}
