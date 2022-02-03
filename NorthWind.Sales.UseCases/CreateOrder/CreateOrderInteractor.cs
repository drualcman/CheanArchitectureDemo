namespace NorthWind.Sales.UseCases.CreateOrder;

public class CreateOrderInteractor : ICreateOrderInputPort
{
    readonly INorthWindSalesCommandsReppository Repository;
    readonly ICreateOrderOutputPort OutputPort;
    readonly IValidator<CreateOrderDto> Validator;

    public CreateOrderInteractor(INorthWindSalesCommandsReppository repository, ICreateOrderOutputPort outputPort, IValidator<CreateOrderDto> validator)
    {
        Repository = repository;
        OutputPort = outputPort;
        Validator = validator;
    }

    public async ValueTask Handle(CreateOrderDto orderDto)
    {
        if (!await Validator.Validate(orderDto))
        {
            string error = string.Join(" ", Validator.Failures.Select(e => $"{e.Key}: {e.Value}").ToArray());
            throw new Exception($"Error de validacionL {error}");
        }
        ExecuteCustomerValidartoinGuard(orderDto.CustomerId);
        ExecuteProductsValidatioinGuard(orderDto);

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
        await Repository.CreateOrder(orderAggregate);
        await Repository.SaveChanges();
        await OutputPort.Handle(orderAggregate.Id);
    }

    void ExecuteCustomerValidartoinGuard(string customerId)
    {
        decimal? currentBalance = Repository.GetCurrentBalance(customerId);
        if (!currentBalance.HasValue)
        {
            throw new Exception("El identificador de cliente proporcionado no existe.");
        }

        if (currentBalance.Value > 0)
        {
            throw new Exception(string.Format("El cliente {0} tiene un adeudo pendiente de {1}",
                customerId, currentBalance.Value));
        }
    }
    void ExecuteProductsValidatioinGuard(CreateOrderDto order)
    {
        Dictionary<int, int> unitsInStock = Repository.GetUnitsInStockOf(order.OrderDetails.Select(p=> p.ProductId).ToList());
        List<(int id, int quantity)> requiredQuantities = order.OrderDetails
            .GroupBy(p => p.ProductId)
            .Select(d => (d.First().ProductId, d.Sum(d => d.Quantity)))
            .ToList();
        StringBuilder errors = new StringBuilder();
        foreach (var product in requiredQuantities)
        {
            if (!unitsInStock.ContainsKey(product.id))
            {
                errors.Append($"El product {product.id} no existe.");
            }
            else
            {
                if (unitsInStock[product.id] < product.quantity)
                {
                    errors.AppendFormat("Cantidad {0} de {1} no suficiente para producto {2}.",
                        product.quantity, unitsInStock[product.id], product.id);
                }
            }
        }
        if (errors.Length > 0)
        {
            throw new Exception(errors.ToString());
        }
    }
}
