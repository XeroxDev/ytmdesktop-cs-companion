using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using NuGet.Versioning;
using XeroxDev.YTMDesktop.Companion.Converter;

namespace XeroxDev.YTMDesktop.Companion.Models.Input.Auth;

/// <summary>
/// The input for the request code endpoint.
/// </summary>
[JsonSerializable(typeof(RequestCodeInput))]
public partial class RequestCodeInput
{
    private string _appId = null!;
    private string _appName = null!;


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

    /// <summary>
    /// The name of your app. Must be between 2 and 48 characters.
    /// </summary>
    [JsonPropertyName("appName"), JsonRequired]
    public string AppName
    {
        get => _appName;
        set
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value), "The app name can't be null or empty");
            _appName = value.Length switch
            {
                < 2 => throw new ArgumentException("The app name must be at least 2 characters long", nameof(value)),
                > 48 => throw new ArgumentException("The app name can't be longer than 48 characters", nameof(value)),
                _ => value
            };
        }
    }

    /// <summary>
    /// The version of your app. Must be semantic versioning compatible.
    /// </summary>
    [JsonPropertyName("appVersion"), JsonRequired, JsonConverter(typeof(SemanticVersionJsonConverter))]
    public SemanticVersion AppVersion { get; set; }

    [GeneratedRegex("^[a-z0-9_\\-]{2,32}$")]
    private static partial Regex AppIdRegex();

    /// <summary>
    /// The Request Code Input.
    /// </summary>
    /// <param name="appId">
    /// The id of your app. Must be all lowercase with only alphanumeric characters, no spaces and between 2 and 32 characters.<br/>
    /// <b>Regex</b>: <c>^[a-z0-9_\\-]{2,32}$</c>
    /// </param>
    /// <param name="appName">The name of your app. Must be between 2 and 48 characters.</param>
    /// <param name="appVersion">The version of your app. Must be semantic versioning compatible.</param>
    /// <exception cref="ArgumentNullException">Thrown when the app id or app name is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when the app id is not valid or the app name is too short or too long.</exception>
    public RequestCodeInput(string appId, string appName, SemanticVersion appVersion)
    {
        AppId = appId;
        AppName = appName;
        AppVersion = appVersion;
    }
}