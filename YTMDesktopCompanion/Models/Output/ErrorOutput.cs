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

using System.Text.Json.Serialization;

#endregion

namespace XeroxDev.YTMDesktop.Companion.Models.Output
{
    /// <summary>
    ///     This class is used to represent the error output of the API.
    /// </summary>
    [JsonSerializable(typeof(ErrorOutput))]
    public class ErrorOutput
    {
        /// <summary>
        ///     The status code of the error. (e.g. 403)
        /// </summary>
        [JsonPropertyName("statusCode")]
        public int? StatusCode { get; set; }

        /// <summary>
        ///     An error code with specific information about the error (e.g. AUTHORIZATION_DISABLED)<br />
        ///     It is not always available.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        ///     The error message title. (e.g. Forbidden)
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; set; }

        /// <summary>
        ///     The error message. (e.g. Authorization requests are disabled)
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        public override string ToString()
        {
            return $"Status Code: {StatusCode}\nCode: {Code}\nError: {Error}\nMessage: {Message}";
        }
    }
}