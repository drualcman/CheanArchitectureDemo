namespace NorthWind.Sales.BusinessObjects.Events;

public class SendEmailWhenSpecialOrderCreateEventHandler : IDomainEventHandler<SpecialOrderCreatedEvent>
{
    readonly IMailService MailService;

    public SendEmailWhenSpecialOrderCreateEventHandler(IMailService mailService)
    {
        MailService = mailService;
    }

    public ValueTask Handle(SpecialOrderCreatedEvent createdOrder) =>
        MailService.SendMailToAdministrator(
            "Order especial creada",
            String.Format("Se ha creado la order {0} con {1} productos.",
                createdOrder.OrderId, createdOrder.ProductsCount));
}
