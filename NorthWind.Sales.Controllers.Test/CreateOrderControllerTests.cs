using NorthWind.Sales.BusinessObjects.DTOs.CreateOrder;
using NorthWind.Sales.Controllers.CreateOrder;
using Xunit;

namespace NorthWind.Sales.Controllers.Test
{
    public class CreateOrderControllerTests
    {
        [Fact]
        public async void CreateOrder_ReturnsIntGreatThan0()
        {
            // Arrange
            FakeCreateOrderPresenter stubPresenter = new FakeCreateOrderPresenter();
            FakeCreateOrderInputPort stubInputPort = new FakeCreateOrderInputPort(stubPresenter);

            CreateOrderController controller = new CreateOrderController(stubInputPort, stubPresenter);

            // Act
            int result = await controller.CreateOrder(new CreateOrderDto());

            // Assert
            Assert.True(result > 0);
        }
    }
}
