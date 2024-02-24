using XeroxDev.YTMDesktop.Companion.Models.Output;

namespace XeroxDev.YTMDesktop.Companion.Exceptions;

/// <summary>
/// Represents an exception that is thrown when an API error occurs.
/// </summary>
[Serializable]
public class ApiException : Exception
{
    public ErrorOutput Error { get; }

    public ApiException(ErrorOutput error) : this(null, null, error)
    {
    }

    public ApiException(string? message, ErrorOutput error) : this(message, null, error)
    {
    }

    public ApiException(string? message, Exception? innerException, ErrorOutput error) : base(message, innerException)
    {
        Error = error;
    }

    public override string ToString()
    {
        return $"""
                {base.ToString()}
                =========
                API Error
                =========
                {Error}
                """;
    }
}