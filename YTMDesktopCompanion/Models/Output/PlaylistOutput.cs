using System.Text.Json.Serialization;
using XeroxDev.YTMDesktop.Companion.Constants;

namespace XeroxDev.YTMDesktop.Companion.Models.Output;

/// <summary>
/// This class is the output from the <see cref="Endpoints.Playlists"/> endpoint. (as a single item, the endpoint will return an array of this)
/// </summary>
[JsonSerializable(typeof(PlaylistOutput))]
public class PlaylistOutput
{
    /// <summary>
    /// The playlist id
    /// </summary>
    [JsonPropertyName("id"), JsonRequired]
    public string Id { get; set; } = null!;

    /// <summary>
    /// The playlist title
    /// </summary>
    [JsonPropertyName("title"), JsonRequired]
    public string Title { get; set; } = null!;
}