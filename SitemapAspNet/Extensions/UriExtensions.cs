using System;

namespace SitemapAspNet.Extensions
{
    /// <summary>
    ///     Extension class for <see cref="System.Uri" />.
    /// </summary>
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