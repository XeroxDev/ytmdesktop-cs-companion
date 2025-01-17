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
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XeroxDev.YTMDesktop.Companion.Constants;
using XeroxDev.YTMDesktop.Companion.Enums;
using XeroxDev.YTMDesktop.Companion.Exceptions;
using XeroxDev.YTMDesktop.Companion.Interfaces;
using XeroxDev.YTMDesktop.Companion.Models.Input;
using XeroxDev.YTMDesktop.Companion.Models.Input.Auth;
using XeroxDev.YTMDesktop.Companion.Models.Output;
using XeroxDev.YTMDesktop.Companion.Models.Output.Auth;
using XeroxDev.YTMDesktop.Companion.Settings;

#endregion

namespace XeroxDev.YTMDesktop.Companion.Clients
{
    /// <summary>
    ///     A REST client to communicate with the companion servers REST API
    /// </summary>
    public class RestClient : IGenericClient
    {
        private readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        ///     A REST client to communicate with the companion servers REST API
        /// </summary>
        /// <param name="settings">The settings to use for the client</param>
        public RestClient(ConnectorSettings settings)
        {
            Settings = settings;
        }

        #region IGenericClient Members

        /// <summary>
        ///     The settings to use for the client
        /// </summary>
        public ConnectorSettings Settings { get; set; }

        /// <inheritdoc />
        public void SetAuthToken(string token)
        {
            Settings.Token = token;
        }

        #endregion

        /// <summary>
        ///     Get the metadata from the API
        /// </summary>
        /// <returns>The metadata from the API</returns>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task<MetadataOutput> GetMetadata()
        {
            return await SendGetRequest<MetadataOutput>(Endpoints.Metadata);
        }

        /// <summary>
        ///     Get the state from the API
        /// </summary>
        /// <returns>The state from the API</returns>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task<StateOutput> GetState()
        {
            return await SendGetRequest<StateOutput>(Endpoints.State, Settings.Token);
        }

        /// <summary>
        ///     Get the playlists from the API
        /// </summary>
        /// <returns>The playlists from the API</returns>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task<PlaylistOutput[]> GetPlaylists()
        {
            return await SendGetRequest<PlaylistOutput[]>(Endpoints.Playlists, Settings.Token) ?? Array.Empty<PlaylistOutput>();
        }

        /// <summary>
        ///     Requests a code to exchange for an auth token
        /// </summary>
        /// <returns>The code to exchange for an auth token</returns>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task<string> GetAuthCode()
        {
            return (
                await SendPostRequest<RequestCodeOutput, RequestCodeInput>(Endpoints.AuthRequestCode, new RequestCodeInput(Settings.AppId, Settings.AppName, Settings.AppVersion))
            )?.Code;
        }

        /// <summary>
        ///     Get the authentication token that is required to access the API.<br />
        ///     <br />
        ///     You should save this token safely and set it either:<br />
        ///     1. in the settings<br />
        ///     2. use the <see cref="SetAuthToken" /> method in this class<br />
        ///     3. use the <see cref="CompanionConnector.SetAuthToken" /> method in the <see cref="CompanionConnector" /> class
        /// </summary>
        /// <param name="code">The code you got from the <see cref="GetAuthCode" /> method</param>
        /// <returns>The authentication token that is required to access the API</returns>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task<string> GetAuthToken(string code)
        {
            return (
                await SendPostRequest<RequestOutput, RequestInput>(Endpoints.AuthRequest, new RequestInput(Settings.AppId, code))
            )?.Token;
        }

        /// <summary>
        ///     Send a GET request to the server
        /// </summary>
        /// <param name="path">The path to send the request to</param>
        /// <param name="token">The token to use for the request</param>
        /// <typeparam name="TResponse">The type of the response</typeparam>
        /// <returns></returns>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        private async Task<TResponse> SendGetRequest<TResponse>(string path, string token = null)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"http://{Settings.Host}:{Settings.Port}{path}"))
            {
                request.Headers.Add("Accept", "application/json");
                if (token != null) request.Headers.Add("Authorization", token);

                return await SendRequest<TResponse>(request);
            }
        }

        private async Task<TResponse> SendPostRequest<TResponse, TBody>(string path, TBody body, string token = null)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"http://{Settings.Host}:{Settings.Port}{path}"))
            {
                request.Headers.Add("Accept", "application/json");
                if (token != null) request.Headers.Add("Authorization", token);
                try
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(body), System.Text.Encoding.UTF8, "application/json");
                }
                catch (Exception e)
                {
                    throw new ApiException("An error occurred while creating the request", e, new ErrorOutput { StatusCode = e.HResult, Error = "Unknown Exception", Message = e.Message });
                }

                return await SendRequest<TResponse>(request);
            }
        }

        private async Task<TResponse> SendRequest<TResponse>(HttpRequestMessage request)
        {
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (Exception e)
            {
                throw new ApiException("An error occurred while sending the request", e, new ErrorOutput
                {
                    StatusCode = e.HResult,
                    Error = "Unknown Exception",
                    Message = e.Message
                });
            }

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<ErrorOutput>(content);
                if (error == null)
                {
                    throw new ApiException(new ErrorOutput
                    {
                        StatusCode = (int)response.StatusCode,
                        Error = response.ReasonPhrase ?? "Unknown Error",
                        Message = response.Content.ReadAsStringAsync().Result
                    });
                }

                error.StatusCode = (int)response.StatusCode;

                throw new ApiException(error);
            }

            // Server still sends for some endpoints no content, so we have to check if the content is empty and return null if it is
            if (string.IsNullOrWhiteSpace(content)) return default;

            // Try parsing the response to ErrorOutput, if it's successful, throw an ApiException
            try
            {
                var errorOutput = JsonConvert.DeserializeObject<ErrorOutput>(content);
                if (errorOutput?.Error != null && errorOutput.Message != null && errorOutput.StatusCode != null && errorOutput.Code != null) throw new ApiException(errorOutput);
            }
            catch (Exception e)
            {
                if (e is ApiException) throw;
            }

            return JsonConvert.DeserializeObject<TResponse>(content);
        }

        #region Commands

        /// <summary>
        ///     Play or pause the current song
        /// </summary>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task PlayPause()
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.PlayPause), Settings.Token);
        }

        /// <summary>
        ///     Play the current song
        /// </summary>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task Play()
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.Play), Settings.Token);
        }

        /// <summary>
        ///     Pause the current song
        /// </summary>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task Pause()
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.Pause), Settings.Token);
        }

        /// <summary>
        ///     Increase the volume
        /// </summary>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task VolumeUp()
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.VolumeUp), Settings.Token);
        }

        /// <summary>
        ///     Decrease the volume
        /// </summary>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task VolumeDown()
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.VolumeDown), Settings.Token);
        }

        /// <summary>
        ///     Set the volume
        /// </summary>
        /// <param name="volume">The volume to set</param>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task SetVolume(int volume)
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.SetVolume, volume), Settings.Token);
        }

        /// <summary>
        ///     Mute the volume
        /// </summary>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task Mute()
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.Mute), Settings.Token);
        }

        /// <summary>
        ///     Unmute the volume
        /// </summary>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task Unmute()
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.Unmute), Settings.Token);
        }

        /// <summary>
        ///     Seek to a specific time in the song
        /// </summary>
        /// <param name="time">The time to seek to</param>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task SeekTo(int time)
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.SeekTo, time), Settings.Token);
        }

        /// <summary>
        ///     Skip to the next song
        /// </summary>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task Next()
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.Next), Settings.Token);
        }

        /// <summary>
        ///     Skip to the previous song
        /// </summary>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task Previous()
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.Previous), Settings.Token);
        }

        /// <summary>
        ///     Set the repeat mode
        /// </summary>
        /// <param name="mode">The mode to set</param>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task SetRepeatMode(ERepeatMode mode)
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.RepeatMode, mode), Settings.Token);
        }

        /// <summary>
        ///     Shuffle the queue
        /// </summary>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task Shuffle()
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.Shuffle), Settings.Token);
        }

        /// <summary>
        ///     Play a song from the queue
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task PlayQueueIndex(int index)
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.PlayQueueIndex, index), Settings.Token);
        }

        /// <summary>
        ///     Toggle like the current song
        /// </summary>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task ToggleLike()
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.ToggleLike), Settings.Token);
        }

        /// <summary>
        ///     Toggle dislike the current song
        /// </summary>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task ToggleDislike()
        {
            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.ToggleDislike), Settings.Token);
        }

        /// <summary>
        ///    Change the video. Either videoId or playlistId or both have to be set. You can also set an url instead. Url is prioritized over videoId and playlistId
        /// </summary>
        /// <param name="videoId">The video id from the video to play</param>
        /// <param name="playlistId">The playlist id from the playlist to play</param>
        /// <param name="url">The url to play</param>
        /// <exception cref="ArgumentNullException">Thrown when videoId and playlistId are null</exception>
        /// <exception cref="ApiException">Thrown when an error occurred while sending the request. Error is either from the server or from the client</exception>
        public async Task ChangeVideo(string videoId = null, string playlistId = null, string url = null)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                var queries = System.Web.HttpUtility.ParseQueryString(new Uri(url).Query);
                videoId = queries.Get("v");
                playlistId = queries.Get("list");
            }

            if (string.IsNullOrWhiteSpace(videoId) && string.IsNullOrWhiteSpace(playlistId) && string.IsNullOrWhiteSpace(url))
                throw new ApiException("videoId, playlistId and url cannot be empty. At least one of them have to be set",
                    new ErrorOutput { StatusCode = 400, Error = "Bad Request", Message = "videoId, playlistId and url cannot be empty. At least one of them have to be set" });

            await SendPostRequest<object, CommandInput>(Endpoints.Command, new CommandInput(ECommand.ChangeVideo, new { videoId, playlistId }), Settings.Token);
        }

        #endregion
    }
}