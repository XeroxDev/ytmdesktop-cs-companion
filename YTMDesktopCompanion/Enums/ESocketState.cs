namespace XeroxDev.YTMDesktop.Companion.Enums;

public enum ESocketState
{
    /// <summary>
    /// If the socket is connecting
    /// </summary>
    Connecting,

    /// <summary>
    /// If the socket is connected
    /// </summary>
    Connected,

    /// <summary>
    /// If the socket is disconnected
    /// </summary>
    Disconnected,

    /// <summary>
    /// The socket seems to be disconnected due to an error
    /// </summary>
    Error
}