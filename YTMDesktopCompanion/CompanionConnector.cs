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
using XeroxDev.YTMDesktop.Companion.Clients;
using XeroxDev.YTMDesktop.Companion.Settings;

#endregion

namespace XeroxDev.YTMDesktop.Companion
{
    /// <summary>
    ///     Companion Connector. This class is the main class of the library. It contains the rest and socket client.<br />
    ///     You can also use the <see cref="Clients.RestClient" /> and <see cref="Clients.SocketClient" /> directly if you want to. But you have to manage the settings update for both clients yourself.
    /// </summary>
    public class CompanionConnector
    {
        /// <summary>
        ///    The companion connector. This class is the main class of the library. It contains the rest and socket client.<br />
        /// </summary>
        /// <param name="settings">The settings for the rest and socket clients.</param>
        public CompanionConnector(ConnectorSettings settings)
        {
            RestClient = new RestClient(settings);
            SocketClient = new SocketClient(settings);
        }

        /// <summary>
        ///     Gets or sets the settings for the rest and socket clients.
        /// </summary>
        public ConnectorSettings Settings
        {
            get => RestClient.Settings ?? SocketClient.Settings ?? throw new NullReferenceException("Settings are not set");
            set
            {
                RestClient.Settings = value;
                SocketClient.Settings = value;
            }
        }

        /// <summary>
        ///     The rest client to make requests to the server.
        /// </summary>
        public RestClient RestClient { get; }

        /// <summary>
        ///     The socket client to make requests to the server.
        /// </summary>
        public SocketClient SocketClient { get; }

        /// <summary>
        ///     Set the settings for the rest and socket clients.
        /// </summary>
        /// <param name="settings">The settings to set</param>
        public void SetSettings(ConnectorSettings settings)
        {
            Settings = settings;
        }

        /// <summary>
        ///     Set the authentication token, so it can be used for further requests.<br />
        ///     This automatically sets the token for both clients and reconnects the socket client if the token changed.
        /// </summary>
        /// <param name="token">The token to set</param>
        public void SetAuthToken(string token)
        {
            RestClient.SetAuthToken(token);
            SocketClient.SetAuthToken(token);
        }
    }
}