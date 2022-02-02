namespace NorthWind.Sales.Validalitor.CreateOrder;

public class CreateOrderDtoValidator : ValidatorWrapper<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(c => c.CustomerId)
            .NotEmpty().WithMessage("Debe proporcionar el identificador del cliente.")
            .Length(5).WithMessage("La longitud del identificador debe de ser 5 caracteres.");

        RuleFor(c => c.ShipAddress)
            .NotEmpty().WithMessage("Debe proporcionar la direccion de envio.")
            .MaximumLength(60).WithMessage("La direccion admite maximo 60 caracteres.");

        RuleFor(c => c.ShipCity)
            .NotEmpty().WithMessage("Debe proporcionar la ciudad de envio.")
            .MinimumLength(3).WithMessage("La ciudad debe tener al menos 3 caracteres.")
            .MaximumLength(15).WithMessage("La ciudad admite maximo 15 caracteres.");

        RuleFor(c => c.ShipCountry)
            .NotEmpty().WithMessage("Debe proporcionar el pais de envio.")
            .MinimumLength(3).WithMessage("El pais debe tener al menos 3 caracteres.")
            .MaximumLength(15).WithMessage("El pais admite maximo 15 caracteres.");

        RuleFor(c => c.ShipPostalCode)
            .MaximumLength(10).WithMessage("El codigo postal admite maximo 15 caracteres.");

        RuleFor(c => c.OrderDetails)
            .Cascade(CascadeMode.Stop)              //stop after first not valid parameter
            .NotNull().WithMessage("Debe de especificar los productos de la ordern.")
            .NotEmpty().WithMessage("Debe especificar al menos un producto de la orden.");
        
        //check each item of OrderDetails
        RuleForEach(o=>o.OrderDetails)
            .SetValidator(new CreateOrderDetailDtoValidator());
    }
}
