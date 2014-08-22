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
        ///     HTTP request informations.
        /// </summary>
        private readonly RequestContext _requestContext;

        /// <summary>
        ///     A collection of routes for the application.
        /// </summary>
        private readonly RouteCollection _routes;

        /// <summary>
        ///     Controller prefix.
        /// </summary>
        private string _controllerPrefix;

        #endregion Fields.

        #region Constructor.

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="requestContext">HTTP request informations.</param>
        /// <param name="routes">A collection of routes for the application.</param>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="requestContext" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="routes" /> is null.</exception>
        public SitemapService(RequestContext requestContext, RouteCollection routes)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext", "The parameter is null.");
            }
            if (routes == null)
            {
                throw new ArgumentNullException("routes", "The parameter is null.");
            }

            _requestContext = requestContext;
            _routes = routes;
        }

        #endregion Constructor.

        #region Methods.

        /// <summary>
        ///     Génére un plan de site selon des ensembles d'attribut <see cref="SitemapAttribute" />
        /// </summary>
        /// <param name="builder">Monteur de plan de site.</param>
        /// <param name="rootUri">Adresse Uri du serveur.</param>
        /// <returns>Chaîne de caractère représentant un document XML valide.</returns>
        /// <exception cref="ArgumentNullException">
        ///     Se lève si <paramref name="builder" /> ou <paramref name="rootUri" /> a la
        ///     valeur null.
        /// </exception>
        public ContentResult Generate(ISitemapBuilder builder, Uri rootUri)
        {
            return Generate(builder, rootUri, ControllerPrefix);
        }

        /// <summary>
        ///     Génére un plan de site selon des ensembles d'attribut <see cref="SitemapAttribute" />
        /// </summary>
        /// <param name="builder">Monteur de plan de site.</param>
        /// <param name="rootUri">Adresse Uri du serveur.</param>
        /// <param name="controllerPrefix">Préfixe des contrôleurs.</param>
        /// <returns>Chaîne de caractère représentant un document XML valide.</returns>
        /// <exception cref="ArgumentNullException">
        ///     Se lève si <paramref name="builder" /> ou <paramref name="rootUri" /> a la
        ///     valeur null.
        /// </exception>
        public ContentResult Generate(ISitemapBuilder builder, Uri rootUri, string controllerPrefix)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder", "The parameter is null.");
            }
            if (rootUri == null)
            {
                throw new ArgumentNullException("rootUri", "The parameter is null.");
            }

            _controllerPrefix = controllerPrefix;
            foreach (var page in _GetPages())
            {
                builder.CreateEntry(page, rootUri);
            }

            return new ContentResult
            {
                Content = builder.Generate(),
                ContentEncoding = Encoding.UTF8,
                ContentType = MediaTypeNames.Text.Xml
            };
        }

        #region Privates.

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