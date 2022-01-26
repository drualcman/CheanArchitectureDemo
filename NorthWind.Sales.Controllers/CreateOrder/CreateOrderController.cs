namespace NorthWind.Sales.Controllers.CreateOrder;

public class CreateOrderController : ICreateOrderController
{
    readonly ICreateOrderInputPort InputPort;
    readonly ICreateOrderPresenter Presenter;

    public CreateOrderController(ICreateOrderInputPort inputPort, ICreateOrderPresenter presenter)
    {
        InputPort = inputPort;
        Presenter = presenter;
    }

    public async ValueTask<int> CreateOrder(CreateOrderDto orderDto)
    {
        await InputPort.Handle(orderDto);
        return Presenter.OrderId;
    }
}
