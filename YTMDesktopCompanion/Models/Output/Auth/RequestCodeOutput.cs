using System.Text.Json.Serialization;
using XeroxDev.YTMDesktop.Companion.Constants;

namespace XeroxDev.YTMDesktop.Companion.Models.Output.Auth;

/// <summary>
/// This class is the output from the <see cref="Endpoints.AuthRequestCode"/> endpoint.
/// </summary>
[JsonSerializable(typeof(RequestCodeOutput))]
public class RequestCodeOutput
{
    /// <summary>
    /// The code to use to authenticate the app.
    /// </summary>
    [JsonPropertyName("code"), JsonRequired]
    public string Code { get; set; } = null!;
}