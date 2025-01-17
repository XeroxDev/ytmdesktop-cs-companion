﻿// This file is part of the YTMDesktopCompanion project.
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

namespace XeroxDev.YTMDesktop.Companion.Enums
{
    /// <summary>
    ///     All the known commands that can be sent to the command endpoint.
    /// </summary>
    public enum ECommand
    {
        /// <summary>
        ///    Play or pause the current song.
        /// </summary>
        PlayPause,

        /// <summary>
        ///   Play the current song.
        /// </summary>
        Play,

        /// <summary>
        ///  Pause the current song.
        /// </summary>
        Pause,

        /// <summary>
        /// Stop the current song.
        /// </summary>
        VolumeUp,

        /// <summary>
        /// Decrease the volume.
        /// </summary>
        VolumeDown,

        /// <summary>
        /// Set the volume to a specific value.
        /// </summary>
        SetVolume,

        /// <summary>
        /// Mute the volume.
        /// </summary>
        Mute,

        /// <summary>
        /// Unmute the volume.
        /// </summary>
        Unmute,

        /// <summary>
        /// Seek to a specific time in the song.
        /// </summary>
        SeekTo,

        /// <summary>
        /// Skip to the next song.
        /// </summary>
        Next,

        /// <summary>
        /// Skip to the previous song.
        /// </summary>
        Previous,

        /// <summary>
        /// Repeat the current song.
        /// </summary>
        RepeatMode,

        /// <summary>
        /// Shuffle the queue.
        /// </summary>
        Shuffle,

        /// <summary>
        /// Add a song to the queue.
        /// </summary>
        PlayQueueIndex,

        /// <summary>
        /// Remove a song from the queue.
        /// </summary>
        ToggleLike,

        /// <summary>
        /// Remove a song from the queue.
        /// </summary>
        ToggleDislike,
        
        /// <summary>
        /// Change the video.
        /// </summary>
        ChangeVideo
    }

    /// <summary>
    ///     Command extensions for the enum <see cref="ECommand" /> to convert it to a string.
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        ///     This method converts the enum to a string that can be used as a command.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static string ToCommandString(this ECommand command)
        {
            return char.ToLowerInvariant(command.ToString()[0]) + command.ToString().Substring(1);
        }
    }
}