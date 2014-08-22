using System;

namespace SitemapAspNet.Extensions
{
    /// <summary>
    ///     Extension class for <see cref="System.Uri" />.
    /// </summary>
    /// <author>Cyril Schumacher</author>
    /// <date>22/08/2014T20:33:21+01:00</date>
    /// <copyright file="/Extensions/UriExtensions.cs">
    ///     The MIT License (MIT)
    /// 
    ///     Copyright (c) 2014, SitemapAspNet by Cyril Schumacher
    /// 
    ///     Permission is hereby granted, free of charge, to any person obtaining a copy
    ///     of this software and associated documentation files (the "Software"), to deal
    ///     in the Software without restriction, including without limitation the rights
    ///     to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    ///     copies of the Software, and to permit persons to whom the Software is
    ///     furnished to do so, subject to the following conditions:
    /// 
    ///     The above copyright notice and this permission notice shall be included in
    ///     all copies or substantial portions of the Software.
    /// 
    ///     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    ///     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    ///     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    ///     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    ///     LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    ///     OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    ///     THE SOFTWARE.
    /// </copyright>
    [CLSCompliant(true)]
    public static class UriExtensions
    {
        #region Methods.

        /// <summary>
        ///     Return the hostname with a protocol.
        /// </summary>
        /// <param name="address">Uri address.</param>
        /// <returns>Uri address represents the hostname with protocol.</returns>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="address" /> is null.</exception>
        public static Uri GetHostNameWithProtocol(this Uri address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address", "The parameter is null.");
            }

            return new Uri(address.GetLeftPart(UriPartial.Authority), UriKind.Absolute);
        }

        #endregion Methods.
    }
}