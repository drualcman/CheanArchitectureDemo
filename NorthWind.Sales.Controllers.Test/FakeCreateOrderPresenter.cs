using NorthWind.Sales.BusinessObjects.DTOs.CreateOrder;
using NorthWind.Sales.BusinessObjects.Interfaces.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Sales.Controllers.Test
{
    public class FakeCreateOrderPresenter : ICreateOrderPresenter
    {
        // framework para crear fakes de pruebas es Moq
        public int OrderId { get; private set; }

        public ValueTask Handle(int orderId)
        {
            OrderId = orderId;
            return ValueTask.CompletedTask;
        }
    }
}
