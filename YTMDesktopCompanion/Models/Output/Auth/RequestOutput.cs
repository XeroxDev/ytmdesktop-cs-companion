using System.Text.Json.Serialization;
using XeroxDev.YTMDesktop.Companion.Constants;

namespace XeroxDev.YTMDesktop.Companion.Models.Output.Auth;

/// <summary>
/// This class is the output from the <see cref="Endpoints.AuthRequest"/> endpoint.
/// </summary>
[JsonSerializable(typeof(RequestOutput))]
public class RequestOutput
{
    /// <summary>
    /// The authorization token that has to be used for all the privileged endpoints.
    /// </summary>
    [JsonPropertyName("token"), JsonRequired]
    public string Token { get; set; } = null!;
}