using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using System.Web.Routing;

namespace SitemapAspNet.Configurations
{
    /// <summary>
    ///     Sitemap Configuration.
    /// </summary>
    [CLSCompliant(true)]
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap")]
    public static class SitemapConfiguration
    {
        #region Constants section.

        /// <summary>
        ///     Default route name.
        /// </summary>
        private const string RouteName = "SitemapService";

        /// <summary>
        ///     Default URL pattern.
        /// </summary>
        private const string UrlPattern = "sitemap";

        #endregion Constants section.

        #region Methods section.

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
                throw new ArgumentNullException("routes", "Value cannot be null.");
            }
            if (string.IsNullOrEmpty(urlPattern))
            {
                throw new ArgumentNullException("urlPattern", "Value cannot be null.");
            }

            _Register(routes, RouteName, urlPattern, new { controller = "Sitemap" });
        }

        #endregion Methods section.
    }
}