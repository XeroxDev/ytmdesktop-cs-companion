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

namespace XeroxDev.YTMDesktop.Companion.Constants
{
    /// <summary>
    ///     This class contains the endpoints for the API.
    /// </summary>
    public static class Endpoints
    {
        /// <summary>
        ///     The supported version. Will be used to build the API path.
        /// </summary>
        public const string SupportedVersion = "v1";

        /// <summary>
        ///     The API path.
        /// </summary>
        public const string Api = "/api/" + SupportedVersion;

        /// <summary>
        ///     The API path for the realtime endpoint.
        /// </summary>
        public const string Realtime = Api + "/realtime";

        /// <summary>
        ///     The API path for the metadata endpoint.
        /// </summary>
        public const string Metadata = "/metadata";

        /// <summary>
        ///     The API path for the auth request endpoint.
        /// </summary>
        public const string AuthRequest = Api + "/auth/request";

        /// <summary>
        ///     The API path for the auth request code endpoint.
        /// </summary>
        public const string AuthRequestCode = Api + "/auth/requestcode";

        /// <summary>
        ///     The API path for the state endpoint.
        /// </summary>
        public const string State = Api + "/state";

        /// <summary>
        ///     The API path for the playlists" endpoint.
        /// </summary>
        public const string Playlists = Api + "/playlists";

        /// <summary>
        ///     The API path for the command endpoint.
        /// </summary>
        public const string Command = Api + "/command";
    }
}