namespace NorthWind.Entities.Interfaces.Events;

/// <summary>
/// Set a class to handler the event defined like IDomainEvnet 
/// </summary>
public interface IDomainEventHandler<EventType>
    where EventType : IDomainEvent  //force only can send classes implement IDomainEvent
{
    ValueTask Handle(EventType eventTypeInstance);
}
