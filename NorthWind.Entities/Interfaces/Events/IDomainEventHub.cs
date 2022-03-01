namespace NorthWind.Entities.Interfaces.Events;

/// <summary>
/// Rise the event to be manage by the event handler
/// </summary>
/// <typeparam name="EventType"></typeparam>
public interface IDomainEventHub<EventType>
    where EventType : IDomainEvent
{
    ValueTask Raise(EventType eventTypeInstance);
}
