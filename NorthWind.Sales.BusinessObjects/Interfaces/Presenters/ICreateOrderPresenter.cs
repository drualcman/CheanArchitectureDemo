namespace NorthWind.Sales.BusinessObjects.Interfaces.Presenters;

public interface ICreateOrderPresenter : ICreateOrderInputPort
{
    int OrderId { get; }
}
