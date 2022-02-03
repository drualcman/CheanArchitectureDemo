using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Sales.Validalitor.CreateOrder
{
    /// <summary>
    /// Manual validator
    /// </summary>
    public class CreateOrderDroIfValidator : Entities.Interfaces.IValidator<CreateOrderDto>
    {
        public IEnumerable<KeyValuePair<string, string>> Failures { get; private set; }

        public ValueTask<bool> Validate(CreateOrderDto instaceToValidate)
        {
            Failures = new List<KeyValuePair<string, string>>();
            if (instaceToValidate.CustomerId == null)
                Failures.Append(new KeyValuePair<string, string>("CustomerId", "Debe de proporcionar un id del cliente"));

            return ValueTask.FromResult(!Failures.Any());
        }
    }
}
