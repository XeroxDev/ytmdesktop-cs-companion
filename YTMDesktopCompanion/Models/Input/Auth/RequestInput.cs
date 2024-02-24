using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using XeroxDev.YTMDesktop.Companion.Models.Output.Auth;

namespace XeroxDev.YTMDesktop.Companion.Models.Input.Auth;

/// <summary>
/// The input for the request code endpoint.
/// </summary>
[JsonSerializable(typeof(RequestInput))]
public partial class RequestInput
{
    private string _appId = null!;

    /// <summary>
    /// The id of your app. Must be all lowercase with only alphanumeric characters, no spaces and between 2 and 32 characters.<br/>
    /// <b>Regex</b>: <c>^[a-z0-9_\\-]{2,32}$</c>
    /// </summary>
    [JsonPropertyName("appId"), JsonRequired]
    public string AppId
    {
        get => _appId;
        set
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value), "The app id can't be null or empty");
            if (!AppIdRegex().IsMatch(value))
                throw new ArgumentException("The app id must be all lowercase with only alphanumeric characters, no spaces and between 2 and 32 characters", nameof(value));
            _appId = value;
        }
    }

    [GeneratedRegex("^[a-z0-9_\\-]{2,32}$")]
    private static partial Regex AppIdRegex();

    /// <summary>
    /// The <see cref="RequestOutput.Token"/> you've received from the server via <see cref="RequestCodeOutput"/>
    /// </summary>
    [JsonPropertyName("code"), JsonRequired]
    public string Code { get; set; }

    /// <summary>
    /// The Request Input.
    /// </summary>
    /// <param name="appId">
    /// The id of your app. Must be all lowercase with only alphanumeric characters, no spaces and between 2 and 32 characters.<br/>
    /// <b>Regex</b>: <c>^[a-z0-9_\\-]{2,32}$</c>
    /// </param>
    /// <param name="code">The <see cref="RequestCodeOutput.Code"/> you've received from the server via <see cref="RequestCodeOutput"/></param>
    /// <exception cref="ArgumentNullException">Thrown when the app id or app name is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when the app id is not valid.</exception>
    public RequestInput(string appId, string code)
    {
        AppId = appId;
        Code = code;
    }
}