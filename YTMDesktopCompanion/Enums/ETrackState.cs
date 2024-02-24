namespace XeroxDev.YTMDesktop.Companion.Enums;

/// <summary>
/// The track state
/// </summary>
public enum ETrackState
{
    /// <summary>
    /// The track state is unknown
    /// </summary>
    Unknown = -1,
    /// <summary>
    /// The track is paused
    /// </summary>
    Paused = 0,
    /// <summary>
    /// The track is playing
    /// </summary>
    Playing = 1,
    /// <summary>
    /// The track is buffering
    /// </summary>
    Buffering = 2
}