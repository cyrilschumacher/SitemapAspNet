using SitemapAspNet.Attributes;
using SitemapAspNet.Builders;
using SitemapAspNet.Models;
using SitemapAspNet.Researcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace SitemapAspNet
{
    /// <summary>
    ///     Sitemap service.
    /// </summary>
    internal sealed class SitemapService
    {
        #region Constants section.

        /// <summary>
        ///     Default Controller prefix.
        /// </summary>
        private const string ControllerPrefix = "Controller";

        #endregion Constants section.

        #region Fields section.

        /// <summary>
        ///     Controller prefix.
        /// </summary>
        private readonly string _controllerPrefix;

        /// <summary>
        ///     HTTP request informations.
        /// </summary>
        private readonly RequestContext _requestContext;

        /// <summary>
        ///     A collection of routes for the application.
        /// </summary>
        private readonly RouteCollection _routes;

        #endregion Fields section.

        #region Constructors section.

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="requestContext">HTTP request informations.</param>
        /// <param name="routes">A collection of routes for the application.</param>
        /// <exception cref="ArgumentNullException">
        ///     Throw if <paramref name="requestContext" /> or <paramref name="routes" /> are
        ///     null.
        /// </exception>
        public SitemapService(RequestContext requestContext, RouteCollection routes)
            : this(requestContext, routes, ControllerPrefix)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="requestContext">HTTP request informations.</param>
        /// <param name="routes">A collection of routes for the application.</param>
        /// <param name="controllerPrefix">Controller prefix.</param>
        /// <exception cref="ArgumentNullException">
        ///     Throw if <paramref name="requestContext" /> or <paramref name="routes" /> are
        ///     null.
        /// </exception>
        public SitemapService(RequestContext requestContext, RouteCollection routes, string controllerPrefix)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext", "Value cannot be null.");
            }
            if (routes == null)
            {
                throw new ArgumentNullException("routes", "Value cannot be null.");
            }

            _controllerPrefix = controllerPrefix;
            _requestContext = requestContext;
            _routes = routes;
        }

        #endregion Constructors section.

        #region Methods section.

        #region Privates.

        /// <summary>
        ///     Create a entries by pages.
        /// </summary>
        /// <param name="builder">Sitemap builder.</param>
        /// <param name="pages">URL entry.</param>
        /// <param name="rootUri">Uri address of server.</param>
        private static void _CreateEntries(ISitemapBuilder builder, IEnumerable<SitemapAttribute> pages, Uri rootUri)
        {
            foreach (var page in pages)
            {
                builder.CreateEntry(page, rootUri);
            }
        }

        /// <summary>
        ///     Return page address.
        /// </summary>
        /// <param name="controllerName">Controller name.</param>
        /// <param name="actionName">Action name.</param>
        /// <returns>Page address.</returns>
        private string _GetPageAddress(string controllerName, string actionName)
        {
            var path = _routes.GetVirtualPath(_requestContext,
                new RouteValueDictionary(
                    new { controller = controllerName.Replace(_controllerPrefix, string.Empty), action = actionName }));
            return (path != null) ? path.VirtualPath : null;
        }

        /// <summary>
        ///     Return pages.
        /// </summary>
        /// <returns>Pages.</returns>
        private IEnumerable<SitemapAttribute> _GetPages()
        {
            return from page in _GetControllersType()
                   where (page.Sitemap != null)
                   select
                       new SitemapAttribute(page.Sitemap.LastModification, page.Sitemap.ChangeFrequently,
                           page.Sitemap.Priority) { Address = _GetPageAddress(page.ControllerName, page.ActionName) };
        }

        /// <summary>
        ///     Return pages informations of <see cref="Controller" />.
        /// </summary>
        /// <returns>Pages informations.</returns>
        private static IEnumerable<SitemapModel> _GetControllersType()
        {
            var application = new ApplicationResearcher(AppDomain.CurrentDomain.GetAssemblies()).Search();
            return new SitemapEngine(new TypeResearcher(application).Search(typeof(Controller))).GetPages();
        }

        #endregion Privates.

        /// <summary>
        ///     Generate a sitemap by attributes <seealso cref="SitemapAttribute" />.
        /// </summary>
        /// <param name="builder">Sitemap builder.</param>
        /// <param name="rootUri">Uri address of server.</param>
        /// <returns>Valid XML document.</returns>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="builder" /> or <paramref name="rootUri" /> are null.</exception>
        public ContentResult Generate(ISitemapBuilder builder, Uri rootUri)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder", "Value cannot be null.");
            }
            if (rootUri == null)
            {
                throw new ArgumentNullException("rootUri", "Value cannot be null.");
            }

            _CreateEntries(builder, _GetPages(), rootUri);
            return new ContentResult
            {
                Content = builder.Generate(),
                ContentEncoding = Encoding.UTF8,
                ContentType = MediaTypeNames.Text.Xml
            };
        }

        #endregion Methods section.
    }
}