using XeroxDev.YTMDesktop.Companion.Clients;
using XeroxDev.YTMDesktop.Companion.Settings;

namespace XeroxDev.YTMDesktop.Companion;

/// <summary>
/// Companion Connector. This class is the main class of the library. It contains the rest and socket client.<br/>
/// You can also use the <see cref="Clients.RestClient"/> and <see cref="Clients.SocketClient"/> directly if you want to. But you have to manage the settings update for both clients yourself.
/// </summary>
public class CompanionConnector
{
    /// <summary>
    /// Gets or sets the settings for the rest and socket clients.
    /// </summary>
    public ConnectorSettings Settings
    {
        get => RestClient.Settings ?? SocketClient.Settings ?? throw new NullReferenceException("Settings are not set");
        set
        {
            RestClient.Settings = value;
            SocketClient.Settings = value;
        }
    }

    /// <summary>
    /// The rest client to make requests to the server.
    /// </summary>
    public RestClient RestClient { get; }

    /// <summary>
    /// The socket client to make requests to the server.
    /// </summary>
    public SocketClient SocketClient { get; }

    public CompanionConnector(ConnectorSettings settings)
    {
        RestClient = new RestClient(settings);
        SocketClient = new SocketClient(settings);
    }

    /// <summary>
    /// Set the settings for the rest and socket clients.
    /// </summary>
    /// <param name="settings">The settings to set</param>
    public void SetSettings(ConnectorSettings settings)
    {
        Settings = settings;
    }

    /// <summary>
    /// Set the authentication token, so it can be used for further requests.<br/>
    /// This automatically sets the token for both clients and reconnects the socket client if the token changed.
    /// </summary>
    /// <param name="token">The token to set</param>
    public void SetAuthToken(string token)
    {
        RestClient.SetAuthToken(token);
        SocketClient.SetAuthToken(token);
    }
}