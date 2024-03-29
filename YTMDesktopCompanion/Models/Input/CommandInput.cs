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

#region


using Newtonsoft.Json;
using XeroxDev.YTMDesktop.Companion.Enums;

#endregion

namespace XeroxDev.YTMDesktop.Companion.Models.Input
{
    /// <summary>
    ///     The input for the command endpoint.
    /// </summary>
    public class CommandInput
    {
        /// <summary>
        ///     The input for the command endpoint.
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <param name="data">The data to send with</param>
        public CommandInput(string command, object data = null)
        {
            Command = command;
            Data = data;
        }

        /// <summary>
        ///     The input for the command endpoint.
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <param name="data">The data to send with</param>
        public CommandInput(ECommand command, object data = null) : this(command.ToCommandString(), data)
        {
        }

        /// <summary>
        ///    The command to execute
        /// </summary>
        [JsonProperty("command")]
        [JsonRequired]
        public string Command { get; set; }

        /// <summary>
        ///   The data to send with
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }
    }
}