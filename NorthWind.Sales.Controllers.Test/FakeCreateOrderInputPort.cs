using NorthWind.Sales.BusinessObjects.DTOs.CreateOrder;
using NorthWind.Sales.BusinessObjects.Interfaces.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Sales.Controllers.Test
{
    public class FakeCreateOrderInputPort : ICreateOrderInputPort
    {
        readonly ICreateOrderOutputPort OutputPort;

        public FakeCreateOrderInputPort(ICreateOrderOutputPort outputPort)
        {
            OutputPort = outputPort;
        }

        public ValueTask Handle(CreateOrderDto orderDto)
        {
            OutputPort.Handle(1);
            return ValueTask.CompletedTask;
        }
    }
}
