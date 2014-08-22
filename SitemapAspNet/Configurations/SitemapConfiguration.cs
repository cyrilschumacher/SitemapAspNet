using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using System.Web.Routing;

namespace SitemapAspNet.Configurations
{
    /// <summary>
    ///     Sitemap Configuration.
    /// </summary>
    /// <author>Cyril Schumacher</author>
    /// <date>08/08/2014T21:13:59+01:00</date>
    /// <copyright file="/Configurations/SitemapConfiguration.cs">
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
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap")]
    public static class SitemapConfiguration
    {
        #region Constants.

        /// <summary>
        ///     Default route name.
        /// </summary>
        private const string RouteName = "SitemapService";

        /// <summary>
        ///     Default URL pattern.
        /// </summary>
        private const string UrlPattern = "sitemap";

        #endregion Constants.

        #region Methods.

        /// <summary>
        ///     Set a URL route.
        /// </summary>
        /// <param name="routes">A collection of routes for the application.</param>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="routes" /> is null.</exception>
        /// <seealso cref="Register(RouteCollection, string)"/>
        public static void Register(RouteCollection routes)
        {
            Register(routes, UrlPattern);
        }

        /// <summary>
        ///     Set a URL route.
        /// </summary>
        /// <param name="routes">A collection of routes for the application.</param>
        /// <param name="urlPattern">The URL pattern for the route.</param>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="routes" /> is null.</exception>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public static void Register(RouteCollection routes, string urlPattern)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes", "The parameter is null.");
            }
            if (string.IsNullOrEmpty(urlPattern))
            {
                throw new ArgumentNullException("urlPattern", "The parameter is null or empty.");
            }

            _Register(routes, RouteName, urlPattern, new { controller = "Sitemap" });
        }

        #region Privates.

        /// <summary>
        ///     Set a URL route.
        /// </summary>
        /// <param name="routes">A collection of routes for the application.</param>
        /// <param name="routeName">Name of route.</param>
        /// <param name="urlPattern">The URL pattern for the route.</param>
        /// <param name="defaultRouteValues">Default route values.</param>
        private static void _Register(RouteCollection routes, string routeName, string urlPattern, object defaultRouteValues)
        {
            routes.MapRoute(routeName, urlPattern, defaultRouteValues);
        }

        #endregion Privates.

        #endregion Methods.
    }
}