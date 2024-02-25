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

using XeroxDev.YTMDesktop.Companion.Settings;

#endregion

namespace XeroxDev.YTMDesktop.Companion.Interfaces
{
    public interface IGenericClient
    {
        ConnectorSettings Settings { get; set; }

        /// <summary>
        ///     Set the authentication token, so it can be used for further requests.<br />
        ///     We <b>recommend</b> to use the <see cref="CompanionConnector.SetAuthToken" /> method in the
        ///     <see cref="CompanionConnector" /> class instead of this method because it also reconnects the socket client if the token changed.
        /// </summary>
        /// <param name="token">The token to set</param>
        void SetAuthToken(string token);
    }
}