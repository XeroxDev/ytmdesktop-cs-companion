using System.Text.Json.Serialization;
using XeroxDev.YTMDesktop.Companion.Constants;
using XeroxDev.YTMDesktop.Companion.Enums;

namespace XeroxDev.YTMDesktop.Companion.Models.Output;

/// <summary>
/// This class is the output from the <see cref="Endpoints.State"/> endpoint. (as a single item, the endpoint will return an array of this)
/// </summary>
[JsonSerializable(typeof(StateOutput))]
public class StateOutput
{
    /// <summary>
    /// The player state
    /// </summary>
    [JsonPropertyName("player"), JsonRequired]
    public Player Player { get; set; } = null!;

    /// <summary>
    /// The video state
    /// </summary>
    [JsonPropertyName("video")]
    public Video? Video { get; set; }

    /// <summary>
    /// The playlist id
    /// </summary>
    [JsonPropertyName("playlistId"), JsonRequired]
    public string PlaylistId { get; set; } = null!;
}

#region Shared

/// <summary>
/// The thumbnail
/// </summary>
[JsonSerializable(typeof(Thumbnail))]
public class Thumbnail
{
    /// <summary>
    /// The url
    /// </summary>
    [JsonPropertyName("url"), JsonRequired]
    public string Url { get; set; } = null!;

    /// <summary>
    /// The width
    /// </summary>
    [JsonPropertyName("width"), JsonRequired]
    public int Width { get; set; }

    /// <summary>
    /// The height
    /// </summary>
    [JsonPropertyName("height"), JsonRequired]
    public int Height { get; set; }
}

#endregion

#region Player classes

/// <summary>
/// The player state
/// </summary>
[JsonSerializable(typeof(Player))]
public class Player
{
    /// <summary>
    /// The track state
    /// </summary>
    [JsonPropertyName("trackState")]
    public ETrackState TrackState { get; set; } = ETrackState.Unknown;

    /// <summary>
    /// The video progress
    /// </summary>
    [JsonPropertyName("videoProgress")]
    public double VideoProgress { get; set; }

    /// <summary>
    /// The volume
    /// </summary>
    [JsonPropertyName("volume")]
    public double Volume { get; set; }

    /// <summary>
    /// If an ad is playing
    /// </summary>
    [JsonPropertyName("adPlaying")]
    public bool AdPlaying { get; set; }

    /// <summary>
    /// The queue
    /// </summary>
    [JsonPropertyName("queue")]
    public Queue? Queue { get; set; }
}

/// <summary>
/// The queue
/// </summary>
[JsonSerializable(typeof(Queue))]
public class Queue
{
    /// <summary>
    /// If autoplay is enabled
    /// </summary>
    [JsonPropertyName("autoplay")]
    public bool Autoplay { get; set; }

    /// <summary>
    /// The queue items
    /// </summary>
    [JsonPropertyName("items"), JsonRequired]
    public QueueItem[] Items { get; set; } = null!;

    /// <summary>
    /// The automix items
    /// </summary>
    [JsonPropertyName("automixItems")]
    public QueueItem[]? AutomixItems { get; set; }

    /// <summary>
    /// If the queue is generating
    /// </summary>
    [JsonPropertyName("isGenerating")]
    public bool IsGenerating { get; set; }

    /// <summary>
    /// If the queue is infinite
    /// </summary>
    [JsonPropertyName("isInfinite")]
    public bool IsInfinite { get; set; }

    /// <summary>
    /// The repeat mode
    /// </summary>
    [JsonPropertyName("repeatMode")]
    public ERepeatMode RepeatMode { get; set; }

    /// <summary>
    /// The selected item index
    /// </summary>
    [JsonPropertyName("selectedItemIndex")]
    public int SelectedItemIndex { get; set; }
}

/// <summary>
/// The queue item
/// </summary>
[JsonSerializable(typeof(QueueItem))]
public class QueueItem
{
    /// <summary>
    /// The thumbnails
    /// </summary>
    [JsonPropertyName("thumbnails"), JsonRequired]
    public Thumbnail[] Thumbnails { get; set; } = null!;

    /// <summary>
    /// The title
    /// </summary>
    [JsonPropertyName("title"), JsonRequired]
    public string Title { get; set; } = null!;

    /// <summary>
    /// The author
    /// </summary>
    [JsonPropertyName("author"), JsonRequired]
    public string Author { get; set; } = null!;

    /// <summary>
    /// The duration
    /// </summary>
    [JsonPropertyName("duration"), JsonRequired]
    public string Duration { get; set; } = null!;

    /// <summary>
    /// If the item is selected
    /// </summary>
    [JsonPropertyName("selected")]
    public bool Selected { get; set; }

    /// <summary>
    /// The video id
    /// </summary>
    [JsonPropertyName("videoId"), JsonRequired]
    public string VideoId { get; set; } = null!;

    /// <summary>
    /// The counterparts
    /// </summary>
    [JsonPropertyName("counterparts")]
    public QueueItem[]? Counterparts { get; set; }
}

#endregion

#region Video classes

/// <summary>
/// The video state
/// </summary>
[JsonSerializable(typeof(Video))]
public class Video
{
    /// <summary>
    /// The author
    /// </summary>
    [JsonPropertyName("author"), JsonRequired]
    public string Author { get; set; } = null!;

    /// <summary>
    /// The channel id
    /// </summary>
    [JsonPropertyName("channelId"), JsonRequired]
    public string ChannelId { get; set; } = null!;

    /// <summary>
    /// The title
    /// </summary>
    [JsonPropertyName("title"), JsonRequired]
    public string Title { get; set; } = null!;

    /// <summary>
    /// The album
    /// </summary>
    [JsonPropertyName("album")]
    public string? Album { get; set; }

    /// <summary>
    /// The album id
    /// </summary>
    [JsonPropertyName("albumId")]
    public string? AlbumId { get; set; }

    /// <summary>
    /// The like status
    /// </summary>
    [JsonPropertyName("likeStatus")]
    public ELikeStatus? LikeStatus { get; set; }

    /// <summary>
    /// The thumbnails
    /// </summary>
    [JsonPropertyName("thumbnails"), JsonRequired]
    public Thumbnail[] Thumbnails { get; set; } = null!;

    /// <summary>
    /// The duration in seconds
    /// </summary>
    [JsonPropertyName("durationSeconds"), JsonRequired]
    public int DurationSeconds { get; set; }

    /// <summary>
    /// The id
    /// </summary>
    [JsonPropertyName("id"), JsonRequired]
    public string Id { get; set; } = null!;
}

#endregion