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

#endregion

namespace XeroxDev.YTMDesktop.Companion.Settings
{
    public class ConnectorSettings
    {
        private string _appId;
        private string _appName;
        private string _host;

        /// <summary>
        ///     The settings for the connector.
        /// </summary>
        /// <param name="host">
        ///     The host name of the server (has to be <b>without</b> a protocol like <c>http://</c> or <c>https://</c> and <b>can't</b> have a trailing slash nor a port)<br />
        ///     <b>Hint</b>: Some operating systems, such as Windows, may use an IPv6 address for localhost. This would result in a connection failure as the server is not listening on the IPv6 address.
        /// </param>
        /// <param name="port">The port of the server.</param>
        /// <param name="appId">
        ///     The id of your app. Must be all lowercase with only alphanumeric characters, no spaces and between 2 and 32 characters.<br />
        ///     <b>Regex</b>: <c>^[a-z0-9_\\-]{2,32}$</c>
        /// </param>
        /// <param name="appName">The name of your app. Must be between 2 and 48 characters.</param>
        /// <param name="appVersion">The version of your app. Must be semantic versioning compatible.</param>
        /// <param name="token">The token to connect to the server (if available).</param>
        /// <exception cref="ArgumentNullException">Thrown when the host, app id or app name is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when the host contains a protocol, a port, the app id is not valid or the app name is too short or too long.</exception>
        public ConnectorSettings(string host, int port, string appId, string appName, string appVersion, string token = null)
        {
            Host = host;
            AppId = appId;
            AppName = appName;
            Port = port;
            AppVersion = appVersion;
            Token = token;
        }

        /// <summary>
        ///     The host name of the server (has to be <b>without</b> a protocol like <c>http://</c> or <c>https://</c> and <b>can't</b> have a trailing slash nor a port)<br />
        ///     <b>Hint</b>: Some operating systems, such as Windows, may use an IPv6 address for localhost. This would result in a connection failure as the server is not listening on the IPv6 address.
        /// </summary>
        public string Host
        {
            get => _host;
            set
            {
                // check if the value is null or empty
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value), "The host can't be null or empty");
                if (value.Contains("://")) throw new ArgumentException("The host can't contain a protocol like http:// or https://", nameof(value));
                if (value.Contains(":")) throw new ArgumentException("The host can't contain a port", nameof(value));
                _host = value.EndsWith("/") ? value.Substring(0, value.Length - 1) : value;
            }
        }

        /// <summary>
        ///     The port of the server.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///     The token to connect to the server (if available).
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///     The id of your app. Must be all lowercase with only alphanumeric characters, no spaces and between 2 and 32 characters.<br />
        ///     <b>Regex</b>: <c>^[a-z0-9_\\-]{2,32}$</c>
        /// </summary>
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
        ///     The name of your app. Must be between 2 and 48 characters.
        /// </summary>
        public string AppName
        {
            get => _appName;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value), "The app name can't be null or empty");
                if (value.Length < 2)
                    throw new ArgumentException("The app name must be at least 2 characters long", nameof(value));
                if (value.Length > 48)
                    throw new ArgumentException("The app name can't be longer than 48 characters", nameof(value));
                _appName = value;
            }
        }

        /// <summary>
        ///     The version of your app. Must be semantic versioning compatible.
        /// </summary>
        public string AppVersion { get; set; }
    }
}