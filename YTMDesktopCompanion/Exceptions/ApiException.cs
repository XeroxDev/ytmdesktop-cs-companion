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

using System;
using XeroxDev.YTMDesktop.Companion.Models.Output;

#endregion

namespace XeroxDev.YTMDesktop.Companion.Exceptions
{
    /// <summary>
    ///     Represents an exception that is thrown when an API error occurs.
    /// </summary>
    [Serializable]
    public class ApiException : Exception
    {
        /// <summary>
        ///    Represents an exception that is thrown when an API error occurs.
        /// </summary>
        /// <param name="error">The error output from the API.</param>
        public ApiException(ErrorOutput error) : this(null, null, error)
        {
        }

        /// <summary>
        ///   Represents an exception that is thrown when an API error occurs.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="error">The error output from the API.</param>
        public ApiException(string message, ErrorOutput error) : this(message, null, error)
        {
        }

        /// <summary>
        ///   Represents an exception that is thrown when an API error occurs.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="error">The error output from the API.</param>
        public ApiException(string message, Exception innerException, ErrorOutput error) : base(message, innerException)
        {
            Error = error;
        }

        /// <summary>
        ///    The error output from the API.
        /// </summary>
        public ErrorOutput Error { get; }

        /// <summary>
        ///   Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{base.ToString()}\n=========\nAPI Error\n=========\n{Error}";
        }
    }
}