namespace NorthWind.EFCore.Repositories.Repositories;

public class LogCommandsRepository : ILogCommandsRepository
{
    readonly NorthWindLogContext Context;

    public LogCommandsRepository(NorthWindLogContext context)
    {
        Context = context;
    }

    public void Add(NorthWind.Entities.ValueObject.DomainLog log)
    {
        Context.Add(new DomainLog
        {
            CreatedDate = log.CreatedDate,
            Description = log.Description,
            UserName = log.UserName
        });
    }

    public async ValueTask SaveChanges()
    {
        try
        {
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new UpdateException(ex.InnerException?.Message ?? ex.Message,
                                      ex.Entries.Select(e => e.Entity.GetType().Name).ToList());
        }
        catch (Exception ex)
        {
            throw new GeneralException(ex.Message, ex);
        }
    }
}
