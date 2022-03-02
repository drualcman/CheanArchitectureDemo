namespace NorthWind.Entities.ValueObject;

public class ApplicationStatusLog
{
    public LogLevel LogLevel { get; }
    public DateTime CreatedDate { get; }
    public string Description { get; }

    public ApplicationStatusLog(LogLevel logLevel, DateTime createdDate, string description)
    {
        LogLevel = logLevel;
        CreatedDate = createdDate;
        Description = description;
    }

    public ApplicationStatusLog(LogLevel logLevel, string description) : this(logLevel, DateTime.Now, description) { }
    public ApplicationStatusLog(string description) : this(LogLevel.Information, DateTime.Now, description) { }
}
