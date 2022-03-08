namespace NorthWind.Entities.ValueObject;

public class DomainLog
{
    public string UserName { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Description { get; set; }

    public DomainLog(DateTime createdDate, string description, string userName) =>
        (CreatedDate, Description, UserName) = (createdDate, description, userName);

    public DomainLog(string description, string userName) : this(DateTime.Now, description, userName) { }
}
