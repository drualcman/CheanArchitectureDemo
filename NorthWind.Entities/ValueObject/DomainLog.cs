namespace NorthWind.Entities.ValueObject;

public class DomainLog
{
    public DateTime CreatedDate { get; set; }
    public string Description { get; set; }

    public DomainLog(DateTime createdDate, string description) =>
        (CreatedDate, Description) = (createdDate, description);

    public DomainLog(string description) : this(DateTime.Now, description) { }
}
