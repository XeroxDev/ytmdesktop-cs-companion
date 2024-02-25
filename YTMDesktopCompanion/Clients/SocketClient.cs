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
using System.Threading.Tasks;
using SocketIOClient;
using SocketIOClient.Transport;
using SocketIOClient.Windows7;
using XeroxDev.YTMDesktop.Companion.Constants;
using XeroxDev.YTMDesktop.Companion.Enums;
using XeroxDev.YTMDesktop.Companion.Interfaces;
using XeroxDev.YTMDesktop.Companion.Models.Output;
using XeroxDev.YTMDesktop.Companion.Settings;

#endregion

namespace XeroxDev.YTMDesktop.Companion.Clients
{
    public class SocketClient : IGenericClient
    {
        private ConnectorSettings _settings;

        /// <summary>
        ///     The socket client
        /// </summary>
        private SocketIOClient.SocketIO _socket;

        public SocketClient(ConnectorSettings settings)
        {
            _settings = settings;
        }

        #region IGenericClient Members

        /// <summary>
        ///     The settings for the socket client.
        /// </summary>
        public ConnectorSettings Settings
        {
            get => _settings;
            set
            {
                if (value is null) throw new ArgumentNullException(nameof(value), "The settings cannot be null.");
                var reconnect = false;
                try
                {
                    reconnect = Settings.Host != value.Host || Settings.Port != value.Port || Settings.Token != value.Token;
                }
                catch
                {
                    // ignored
                }

                _settings = value;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                if (reconnect && _socket != null) Connect();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        /// <inheritdoc />
        public void SetAuthToken(string token)
        {
            var settings = Settings;
            settings.Token = token;
            Settings = settings;
        }

        #endregion

        /// <summary>
        ///     Get the whole socket object. Use with caution!<br />
        ///     Useful for custom things that are not implemented in the library yet.
        /// </summary>
        /// <returns>The socket object</returns>
        public SocketIOClient.SocketIO GetSocket()
        {
            return _socket;
        }

        /// <summary>
        ///     Connect to the socket server
        /// </summary>
        public async Task Connect()
        {
            try
            {
                if (_socket != null)
                {
                    await _socket.DisconnectAsync();
                    _socket.Dispose();
                    _socket = null;
                    OnConnectionChange(this, ESocketState.Disconnected);
                }

                OnConnectionChange(this, ESocketState.Connecting);

                _socket = new SocketIOClient.SocketIO($"http://{Settings.Host}:{Settings.Port}{Endpoints.Realtime}", new SocketIOOptions
                {
                    Transport = TransportProtocol.WebSocket,
                    Auth = Settings.Token
                });
                _socket.ClientWebSocketProvider = () => new SystemNetWebSocketsClientWebSocket();

                _socket.OnConnected += (sender, _) => OnConnectionChange(sender ?? this, ESocketState.Connected);
                _socket.OnDisconnected += (sender, _) => OnConnectionChange(sender ?? this, ESocketState.Disconnected);
                _socket.OnError += (sender, args) => OnError(sender ?? this, new Exception(args));
                _socket.OnReconnectError += (sender, args) => OnError(sender ?? this, args);
                _socket.OnReconnected += (sender, _) => OnConnectionChange(sender ?? this, ESocketState.Connected);
                _socket.OnReconnectAttempt += (sender, _) => OnConnectionChange(sender ?? this, ESocketState.Connecting);
                _socket.On("state-update", data => OnStateChange(this, data.GetValue<StateOutput>()));
                _socket.On("playlist-created", data => OnPlaylistCreated(this, data.GetValue<PlaylistOutput>()));
                _socket.On("playlist-delete", data => OnPlaylistDeleted(this, data.GetValue<string>()));
            }
            catch (Exception ex)
            {
                OnError(this, ex);
            }
        }

        #region Events

        /// <summary>
        ///     The event that is raised when the client receives errors.
        /// </summary>
        public event EventHandler<Exception> OnError = delegate { };

        /// <summary>
        ///     The event that is raised when the socket connection is changed.
        /// </summary>
        public event EventHandler<ESocketState> OnConnectionChange = delegate { };

        /// <summary>
        ///     The event that is raised when the YTMDesktop State has changed.
        /// </summary>
        public event EventHandler<StateOutput> OnStateChange = delegate { };

        /// <summary>
        ///     The event that is raised when a playlist was created
        /// </summary>
        public event EventHandler<PlaylistOutput> OnPlaylistCreated = delegate { };

        /// <summary>
        ///     The event that is raised when a playlist was deleted
        /// </summary>
        public event EventHandler<string> OnPlaylistDeleted = delegate { };

        #endregion
    }
}