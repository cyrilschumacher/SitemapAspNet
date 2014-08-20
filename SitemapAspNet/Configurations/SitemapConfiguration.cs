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
    [CLSCompliant(true)]
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap")]
    public static class SitemapConfiguration
    {
        #region Constantes.

        /// <summary>
        ///     Default route name.
        /// </summary>
        private const string RouteName = "SitemapService";

        /// <summary>
        ///     Default URL pattern.
        /// </summary>
        private const string UrlPattern = "sitemap";

        #endregion Constantes.

        #region Méthodes.

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

        #endregion Méthodes.
    }
}