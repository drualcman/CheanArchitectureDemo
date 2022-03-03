namespace NorthWind.EFCore.Repositories.Entities;

public class DomainLog
{
    public int Id { get; set; } 
    public DateTime CreatedDate { get; set; }
    public string Description { get; set; }
}
