namespace NorthWind.Sales.BusinessObjects.Specifications;

public class SpecialOrderSpecification : Specification<OrderAggregate>
{
    public override Expression<Func<OrderAggregate, bool>> ConditionExpression =>
        orderAggregate => orderAggregate.OrderDetails.Count > 3;
}
