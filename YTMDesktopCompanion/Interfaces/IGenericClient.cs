using XeroxDev.YTMDesktop.Companion.Settings;

namespace XeroxDev.YTMDesktop.Companion.Interfaces;

public interface IGenericClient
{
    public ConnectorSettings Settings { get; set; }

    /// <summary>
    /// Set the authentication token, so it can be used for further requests.<br/>
    /// We <b>recommend</b> to use the <see cref="CompanionConnector.SetAuthToken"/> method in the
    /// <see cref="CompanionConnector"/> class instead of this method because it also reconnects the socket client if the token changed.
    /// </summary>
    /// <param name="token">The token to set</param>
    public void SetAuthToken(string token);
}