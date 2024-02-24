namespace XeroxDev.YTMDesktop.Companion.Constants;

/// <summary>
/// This class contains the endpoints for the API.
/// </summary>
public static class Endpoints
{
    /// <summary>
    /// The supported version. Will be used to build the API path.
    /// </summary>
    public const string SupportedVersion = "v1";

    /// <summary>
    /// The API path.
    /// </summary>
    public const string Api = "/api/" + Endpoints.SupportedVersion;

    /// <summary>
    /// The API path for the realtime endpoint.
    /// </summary>
    public const string Realtime = Endpoints.Api + "/realtime";

    /// <summary>
    /// The API path for the metadata endpoint.
    /// </summary>
    public const string Metadata = "/metadata";

    /// <summary>
    /// The API path for the auth request endpoint.
    /// </summary>
    public const string AuthRequest = Endpoints.Api + "/auth/request";

    /// <summary>
    /// The API path for the auth request code endpoint.
    /// </summary>
    public const string AuthRequestCode = Endpoints.Api + "/auth/requestcode";

    /// <summary>
    /// The API path for the state endpoint.
    /// </summary>
    public const string State = Endpoints.Api + "/state";

    /// <summary>
    /// The API path for the playlists" endpoint.
    /// </summary>
    public const string Playlists = Endpoints.Api + "/playlists";

    /// <summary>
    /// The API path for the command endpoint.
    /// </summary>
    public const string Command = Endpoints.Api + "/command";
}