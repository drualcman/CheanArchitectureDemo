namespace NorthWind.Entities.Exceptions;

/// <summary>
/// Generic Exception
/// </summary>
public class GeneralException : Exception
{
    /// <summary>
    /// A human-readable explanation specific to this occurrence of the problem.
    /// </summary>
    public string Detail { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public GeneralException() { }

    /// <summary>
    /// Constructor with message
    /// </summary>
    /// <param name="message"></param>
    public GeneralException(string message) : base(message) { }

    /// <summary>
    /// Constructor with message and exception
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public GeneralException(string message, Exception innerException) : base(message, innerException) =>
        Detail = innerException?.Message;

    /// <summary>
    /// Constructur with message and detail
    /// </summary>
    /// <param name="message"></param>
    /// <param name="detail"></param>
    public GeneralException(string message, string detail) : base(message) =>
        Detail = detail;
}
