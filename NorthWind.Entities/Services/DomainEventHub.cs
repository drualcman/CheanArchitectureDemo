namespace NorthWind.Entities.Services;

/// <summary>
/// Implement the hib to rise events for the domain
/// </summary>
public class DomainEventHub<EventType> : IDomainEventHub<EventType>
    where EventType : IDomainEvent
{
    readonly IEnumerable<IDomainEventHandler<EventType>> EventHandlers;

    public DomainEventHub(IEnumerable<IDomainEventHandler<EventType>> eventHandlers)
    {
        EventHandlers = eventHandlers;
    }

    public async ValueTask Raise(EventType eventTypeInstance)
    {
        foreach (IDomainEventHandler<EventType> handler in EventHandlers)
        {
            await handler.Handle(eventTypeInstance);
        }
    }
}
