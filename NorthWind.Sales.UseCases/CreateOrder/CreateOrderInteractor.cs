

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
}
