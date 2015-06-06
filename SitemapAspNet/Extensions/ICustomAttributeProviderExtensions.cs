using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SitemapAspNet.Extensions
{
    /// <summary>
    ///     Extension class for <see cref="ICustomAttributeProvider" />.
    /// </summary>
    [CLSCompliant(true)]
    public static class ICustomAttributeProviderExtensions
    {
        #region Methods section.

        /// <summary>
        ///     Return attributes by type.
        /// </summary>
        /// <typeparam name="T">Attributes type.</typeparam>
        /// <param name="customAttributeProvider">Attribute.</param>
        /// <returns>Attributes.</returns>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="customAttributeProvider" /> is null.</exception>
        public static IEnumerable<T> GetCustomAttributesByType<T>(this ICustomAttributeProvider customAttributeProvider)
            where T : class
        {
            if (customAttributeProvider == null)
            {
                throw new ArgumentNullException("customAttributeProvider");
            }

            return from customAttribute in customAttributeProvider.GetCustomAttributes(true).OfType<T>()
                   select customAttribute;
        }

        #endregion Methods section.
    }
}