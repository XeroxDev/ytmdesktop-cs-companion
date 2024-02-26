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

using System;

using System.Text.RegularExpressions;
using Newtonsoft.Json;
using XeroxDev.YTMDesktop.Companion.Models.Output.Auth;

#endregion

namespace XeroxDev.YTMDesktop.Companion.Models.Input.Auth
{
    /// <summary>
    ///     The input for the request code endpoint.
    /// </summary>
    public class RequestInput
    {
        private string _appId;

        /// <summary>
        ///     The Request Input.
        /// </summary>
        /// <param name="appId">
        ///     The id of your app. Must be all lowercase with only alphanumeric characters, no spaces and between 2 and 32 characters.<br />
        ///     <b>Regex</b>: <c>^[a-z0-9_\\-]{2,32}$</c>
        /// </param>
        /// <param name="code">The <see cref="RequestCodeOutput.Code" /> you've received from the server via <see cref="RequestCodeOutput" /></param>
        /// <exception cref="ArgumentNullException">Thrown when the app id or app name is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when the app id is not valid.</exception>
        public RequestInput(string appId, string code)
        {
            AppId = appId;
            Code = code;
        }

        /// <summary>
        ///     The id of your app. Must be all lowercase with only alphanumeric characters, no spaces and between 2 and 32 characters.<br />
        ///     <b>Regex</b>: <c>^[a-z0-9_\\-]{2,32}$</c>
        /// </summary>
        [JsonProperty("appId")]
        [JsonRequired]
        public string AppId
        {
            get => _appId;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value), "The app id can't be null or empty");
                if (!Regex.IsMatch(value, "^[a-z0-9_\\-]{2,32}$"))
                    throw new ArgumentException("The app id must be all lowercase with only alphanumeric characters, no spaces and between 2 and 32 characters", nameof(value));
                _appId = value;
            }
        }

        /// <summary>
        ///     The <see cref="RequestOutput.Token" /> you've received from the server via <see cref="RequestCodeOutput" />
        /// </summary>
        [JsonProperty("code")]
        [JsonRequired]
        public string Code { get; set; }
    }
}