using System.Text.Json.Serialization;

namespace XeroxDev.YTMDesktop.Companion.Models.Output;

/// <summary>
/// This class is used to represent the error output of the API.
/// </summary>
[JsonSerializable(typeof(ErrorOutput))]
public class ErrorOutput
{
    /// <summary>
    /// The status code of the error. (e.g. 403)
    /// </summary>
    [JsonPropertyName("statusCode")]
    public int? StatusCode { get; set; }

    /// <summary>
    /// An error code with specific information about the error (e.g. AUTHORIZATION_DISABLED)<br/>
    /// It is not always available.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// The error message title. (e.g. Forbidden)
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>
    /// The error message. (e.g. Authorization requests are disabled)
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    public override string ToString()
    {
        return $"Status Code: {StatusCode}\nCode: {Code}\nError: {Error}\nMessage: {Message}";
    }
}