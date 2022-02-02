namespace NorthWind.Sales.Validalitor.CreateOrder;

public class CreateOrderDetailDtoValidator : ValidatorWrapper<CreateOrderDetailDto>
{
    public CreateOrderDetailDtoValidator()
    {
        RuleFor(d => d.ProductId).GreaterThan(0).WithMessage("Debe especificar el identificador el producto");
        RuleFor(d => d.UnitPrice).GreaterThan(0).WithMessage("Debe especificar el precio el producto");
        RuleFor(d => d.Quantity).GreaterThan((short)0).WithMessage("Debe especificar la cantidad el producto");
    }
}
