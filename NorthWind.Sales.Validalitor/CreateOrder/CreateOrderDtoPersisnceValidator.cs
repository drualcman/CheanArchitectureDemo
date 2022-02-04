namespace NorthWind.Sales.Validalitor.CreateOrder;

public class CreateOrderDtoPersisnceValidator : Entities.Interfaces.IValidator<CreateOrderDto>
{
    readonly INorthWindSalesCommandsReppository Repository;

    public CreateOrderDtoPersisnceValidator(INorthWindSalesCommandsReppository repository) =>
        Repository = repository;

    public IEnumerable<KeyValuePair<string, string>> Failures { get; private set; }

    public ValueTask<bool> Validate(CreateOrderDto orderDto)
    {
        List<KeyValuePair<string, string>> errors = new List<KeyValuePair<string, string>>();
        KeyValuePair<string, string>? customerValidation = ExecuteCustomerValidation(orderDto.CustomerId);
        if (customerValidation != null) Failures = new List<KeyValuePair<string, string>>() { (customerValidation.Value) };
        else Failures = new List<KeyValuePair<string, string>>(ExecuteProductsValidation(orderDto));
        return ValueTask.FromResult(Failures is null || !Failures.Any());
    }

    KeyValuePair<string, string>? ExecuteCustomerValidation(string customerId)
    {
        KeyValuePair<string, string>? result = null;
        decimal? currentBalance = Repository.GetCurrentBalance(customerId);
        if (currentBalance == null)
        {
            result = new KeyValuePair<string, string>("customerId", "El identificador de cliente proporcionado no existe.");
        }
        else if (currentBalance.Value > 0)
        {
            result = new KeyValuePair<string, string>("CustomerId", string.Format("El cliente {0} tiene un adeudo pendiente de {1}",
                customerId, currentBalance.Value));
        }
        return result;
    }

    List<KeyValuePair<string, string>> ExecuteProductsValidation(CreateOrderDto order)
    {
        List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
        Dictionary<int, int> unitsInStock = Repository.GetUnitsInStockOf(order.OrderDetails.Select(p => p.ProductId).ToList());
        List<(int id, int quantity)> requiredQuantities = order.OrderDetails
            .GroupBy(p => p.ProductId)
            .Select(d => (d.First().ProductId, d.Sum(d => d.Quantity)))
            .ToList();

        foreach (var product in requiredQuantities)
        {
            if (!unitsInStock.ContainsKey(product.id))
            {
                result.Add(new KeyValuePair<string, string>("ProductId", $"El product {product.id} no existe."));
            }
            else if (unitsInStock[product.id] < product.quantity)
            {
                result.Add(new KeyValuePair<string, string>("Quantity", string.Format("Cantidad {0} de {1} no suficiente para producto {2}.",
                    product.quantity, unitsInStock[product.id], product.id)));
            }
        }
        return result;
    }
}
