namespace NorthWind.Entities.Interfaces;

public interface ILogCommandsRepository : IUnitOfWork
{
    void Add(DomainLog log);
}
