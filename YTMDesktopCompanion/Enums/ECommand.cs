namespace XeroxDev.YTMDesktop.Companion.Enums;

/// <summary>
/// All the known commands that can be sent to the command endpoint.
/// </summary>
public enum ECommand
{
    PlayPause,
    Play,
    Pause,
    VolumeUp,
    VolumeDown,
    SetVolume,
    Mute,
    Unmute,
    SeekTo,
    Next,
    Previous,
    RepeatMode,
    Shuffle,
    PlayQueueIndex,
    ToggleLike,
    ToggleDislike,
}

/// <summary>
/// Command extensions for the enum <see cref="ECommand"/> to convert it to a string.
/// </summary>
public static class CommandExtensions
{
    /// <summary>
    /// This method converts the enum to a string that can be used as a command.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public static string ToCommandString(this ECommand command) => char.ToLowerInvariant(command.ToString()[0]) + command.ToString()[1..];
}