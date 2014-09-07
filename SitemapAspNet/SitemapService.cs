using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using SitemapAspNet.Attributes;
using SitemapAspNet.Builders;
using SitemapAspNet.Models;
using SitemapAspNet.Researcher;

namespace SitemapAspNet
{
    /// <summary>
    ///     Sitemap service.
    /// </summary>
    /// <author>Cyril Schumacher</author>
    /// <date>15/02/2014T18:28:54+01:00</date>
    /// <copyright file="/SitemapService.cs">
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
    internal sealed class SitemapService
    {
        #region Constants.

        /// <summary>
        ///     Default Controller prefix.
        /// </summary>
        private const string ControllerPrefix = "Controller";

        #endregion Constants.

        #region Fields.

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

        #endregion Fields.

        #region Constructor.

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

        #endregion Constructor.

        #region Methods.

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
                    new {controller = controllerName.Replace(_controllerPrefix, string.Empty), action = actionName}));
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
                        page.Sitemap.Priority) {Address = _GetPageAddress(page.ControllerName, page.ActionName)};
        }

        /// <summary>
        ///     Return pages informations of <see cref="Controller" />.
        /// </summary>
        /// <returns>Pages informations.</returns>
        private static IEnumerable<SitemapModel> _GetControllersType()
        {
            var application = new ApplicationResearcher(AppDomain.CurrentDomain.GetAssemblies()).Search();
            return new SitemapEngine(new TypeResearcher(application).Search(typeof (Controller))).GetPages();
        }

        #endregion Privates.

        #endregion Methods.
    }
}