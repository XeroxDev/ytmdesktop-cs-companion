// This file is part of the YTMDesktopCompanion project.
// 
// Copyright (c) 2024 Dominic Ris
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#region


using Newtonsoft.Json;
using XeroxDev.YTMDesktop.Companion.Constants;
using XeroxDev.YTMDesktop.Companion.Enums;

#endregion

namespace XeroxDev.YTMDesktop.Companion.Models.Output
{
    /// <summary>
    ///     This class is the output from the <see cref="Endpoints.State" /> endpoint. (as a single item, the endpoint will return an array of this)
    /// </summary>
    public class StateOutput
    {
        /// <summary>
        ///     The player state
        /// </summary>
        [JsonProperty("player")]
        [JsonRequired]
        public Player Player { get; set; } = null;

        /// <summary>
        ///     The video state
        /// </summary>
        [JsonProperty("video")]
        public Video Video { get; set; }

        /// <summary>
        ///     The playlist id
        /// </summary>
        [JsonProperty("playlistId")]
        [JsonRequired]
        public string PlaylistId { get; set; } = null;
    }

    #region Shared

    /// <summary>
    ///     The thumbnail
    /// </summary>
    public class Thumbnail
    {
        /// <summary>
        ///     The url
        /// </summary>
        [JsonProperty("url")]
        [JsonRequired]
        public string Url { get; set; } = null;

        /// <summary>
        ///     The width
        /// </summary>
        [JsonProperty("width")]
        [JsonRequired]
        public int Width { get; set; }

        /// <summary>
        ///     The height
        /// </summary>
        [JsonProperty("height")]
        [JsonRequired]
        public int Height { get; set; }
    }

    #endregion

    #region Player classes

    /// <summary>
    ///     The player state
    /// </summary>
    public class Player
    {
        /// <summary>
        ///     The track state
        /// </summary>
        [JsonProperty("trackState")]
        public ETrackState TrackState { get; set; } = ETrackState.Unknown;

        /// <summary>
        ///     The video progress
        /// </summary>
        [JsonProperty("videoProgress")]
        public double VideoProgress { get; set; }

        /// <summary>
        ///     The volume
        /// </summary>
        [JsonProperty("volume")]
        public double Volume { get; set; }

        /// <summary>
        ///     If an ad is playing
        /// </summary>
        [JsonProperty("adPlaying")]
        public bool AdPlaying { get; set; }

        /// <summary>
        ///     The queue
        /// </summary>
        [JsonProperty("queue")]
        public Queue Queue { get; set; }
    }

    /// <summary>
    ///     The queue
    /// </summary>
    public class Queue
    {
        /// <summary>
        ///     If autoplay is enabled
        /// </summary>
        [JsonProperty("autoplay")]
        public bool Autoplay { get; set; }

        /// <summary>
        ///     The queue items
        /// </summary>
        [JsonProperty("items")]
        [JsonRequired]
        public QueueItem[] Items { get; set; } = null;

        /// <summary>
        ///     The automix items
        /// </summary>
        [JsonProperty("automixItems")]
        public QueueItem[] AutomixItems { get; set; }

        /// <summary>
        ///     If the queue is generating
        /// </summary>
        [JsonProperty("isGenerating")]
        public bool IsGenerating { get; set; }

        /// <summary>
        ///     If the queue is infinite
        /// </summary>
        [JsonProperty("isInfinite")]
        public bool IsInfinite { get; set; }

        /// <summary>
        ///     The repeat mode
        /// </summary>
        [JsonProperty("repeatMode")]
        public ERepeatMode RepeatMode { get; set; }

        /// <summary>
        ///     The selected item index
        /// </summary>
        [JsonProperty("selectedItemIndex")]
        public int SelectedItemIndex { get; set; }
    }

    /// <summary>
    ///     The queue item
    /// </summary>
    public class QueueItem
    {
        /// <summary>
        ///     The thumbnails
        /// </summary>
        [JsonProperty("thumbnails")]
        [JsonRequired]
        public Thumbnail[] Thumbnails { get; set; } = null;

        /// <summary>
        ///     The title
        /// </summary>
        [JsonProperty("title")]
        [JsonRequired]
        public string Title { get; set; } = null;

        /// <summary>
        ///     The author
        /// </summary>
        [JsonProperty("author")]
        [JsonRequired]
        public string Author { get; set; } = null;

        /// <summary>
        ///     The duration
        /// </summary>
        [JsonProperty("duration")]
        [JsonRequired]
        public string Duration { get; set; } = null;

        /// <summary>
        ///     If the item is selected
        /// </summary>
        [JsonProperty("selected")]
        public bool Selected { get; set; }

        /// <summary>
        ///     The video id
        /// </summary>
        [JsonProperty("videoId")]
        [JsonRequired]
        public string VideoId { get; set; } = null;

        /// <summary>
        ///     The counterparts
        /// </summary>
        [JsonProperty("counterparts")]
        public QueueItem[] Counterparts { get; set; }
    }

    #endregion

    #region Video classes

    /// <summary>
    ///     The video state
    /// </summary>
    public class Video
    {
        /// <summary>
        ///     The author
        /// </summary>
        [JsonProperty("author")]
        [JsonRequired]
        public string Author { get; set; } = null;

        /// <summary>
        ///     The channel id
        /// </summary>
        [JsonProperty("channelId")]
        [JsonRequired]
        public string ChannelId { get; set; } = null;

        /// <summary>
        ///     The title
        /// </summary>
        [JsonProperty("title")]
        [JsonRequired]
        public string Title { get; set; } = null;

        /// <summary>
        ///     The album
        /// </summary>
        [JsonProperty("album")]
        public string Album { get; set; }

        /// <summary>
        ///     The album id
        /// </summary>
        [JsonProperty("albumId")]
        public string AlbumId { get; set; }

        /// <summary>
        ///     The like status
        /// </summary>
        [JsonProperty("likeStatus")]
        public ELikeStatus? LikeStatus { get; set; }

        /// <summary>
        ///     The thumbnails
        /// </summary>
        [JsonProperty("thumbnails")]
        [JsonRequired]
        public Thumbnail[] Thumbnails { get; set; } = null;

        /// <summary>
        ///     The duration in seconds
        /// </summary>
        [JsonProperty("durationSeconds")]
        [JsonRequired]
        public int DurationSeconds { get; set; }

        /// <summary>
        ///     The id
        /// </summary>
        [JsonProperty("id")]
        [JsonRequired]
        public string Id { get; set; } = null;
    }

    #endregion
}