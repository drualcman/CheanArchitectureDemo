namespace NorthWind.Entities.ValueObject;

public record struct ProblemDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        Dictionary<string, List<string>> InvalidParams);
