using System.Text.RegularExpressions;
using NuGet.Versioning;

namespace XeroxDev.YTMDesktop.Companion.Settings;

public partial class ConnectorSettings
{
    private string _host = null!;
    private string _appId = null!;
    private string _appName = null!;

    /// <summary>
    /// The host name of the server (has to be <b>without</b> a protocol like <c>http://</c> or <c>https://</c> and <b>can't</b> have a trailing slash nor a port)<br/>
    /// <b>Hint</b>: Some operating systems, such as Windows, may use an IPv6 address for localhost. This would result in a connection failure as the server is not listening on the IPv6 address.
    /// </summary>
    public string Host
    {
        get => _host;
        set
        {
            // check if the value is null or empty
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value), "The host can't be null or empty");
            if (value.Contains("://")) throw new ArgumentException("The host can't contain a protocol like http:// or https://", nameof(value));
            if (value.Contains(':')) throw new ArgumentException("The host can't contain a port", nameof(value));
            _host = value.EndsWith('/') ? value[..^1] : value;
        }
    }

    /// <summary>
    /// The port of the server.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// The token to connect to the server (if available).
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// The id of your app. Must be all lowercase with only alphanumeric characters, no spaces and between 2 and 32 characters.<br/>
    /// <b>Regex</b>: <c>^[a-z0-9_\\-]{2,32}$</c>
    /// </summary>
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
    public SemanticVersion AppVersion { get; set; }

    /// <summary>
    /// The settings for the connector.
    /// </summary>
    /// <param name="host">
    /// The host name of the server (has to be <b>without</b> a protocol like <c>http://</c> or <c>https://</c> and <b>can't</b> have a trailing slash nor a port)<br/>
    /// <b>Hint</b>: Some operating systems, such as Windows, may use an IPv6 address for localhost. This would result in a connection failure as the server is not listening on the IPv6 address.
    /// </param>
    /// <param name="port">The port of the server.</param>
    /// <param name="appId">
    /// The id of your app. Must be all lowercase with only alphanumeric characters, no spaces and between 2 and 32 characters.<br/>
    /// <b>Regex</b>: <c>^[a-z0-9_\\-]{2,32}$</c>
    /// </param>
    /// <param name="appName">The name of your app. Must be between 2 and 48 characters.</param>
    /// <param name="appVersion">The version of your app. Must be semantic versioning compatible.</param>
    /// <param name="token">The token to connect to the server (if available).</param>
    /// <exception cref="ArgumentNullException">Thrown when the host, app id or app name is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when the host contains a protocol, a port, the app id is not valid or the app name is too short or too long.</exception>
    public ConnectorSettings(string host, int port, string appId, string appName, SemanticVersion appVersion, string? token = null)
    {
        Host = host;
        AppId = appId;
        AppName = appName;
        Port = port;
        AppVersion = appVersion;
        Token = token;
    }

    [GeneratedRegex("^[a-z0-9_\\-]{2,32}$")]
    private static partial Regex AppIdRegex();
}