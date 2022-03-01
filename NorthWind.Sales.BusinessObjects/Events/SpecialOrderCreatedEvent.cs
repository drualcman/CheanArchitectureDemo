namespace NorthWind.Sales.BusinessObjects.Events;

/// <summary>
/// Implementing Domain Event
/// </summary>
public struct SpecialOrderCreatedEvent : IDomainEvent
{
    public int OrderId { get;  }
    public int ProductsCount { get;  }

    public SpecialOrderCreatedEvent(int orderId, int productsCount)
    {
        OrderId = orderId;
        ProductsCount = productsCount;
    }
}
