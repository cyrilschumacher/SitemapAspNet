using SitemapAspNet.Builders;
using SitemapAspNet.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using System.Web.Routing;

namespace SitemapAspNet.Controllers
{
    /// <summary>
    ///     Sitemap Controller.
    /// </summary>
    /// <author>Cyril Schumacher</author>
    /// <date>08/08/2014T21:17:52+01:00</date>
    /// <copyright file="/Controllers/SitemapController.cs">
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
    public class SitemapController : Controller
    {
        #region Fields.

        /// <summary>
        ///     Sitemap builder.
        /// </summary>
        private readonly ISitemapBuilder _sitemapBuilder;

        #endregion Fields.

        #region Constructor.

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <seealso cref="SitemapController(ISitemapBuilder)"/>
        public SitemapController()
            : this(new XmlSitemapBuilder())
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="sitemapBuilder">Sitemap builder.</param>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="sitemapBuilder" /> is null.</exception>
        public SitemapController(ISitemapBuilder sitemapBuilder)
        {
            if (sitemapBuilder == null)
            {
                throw new ArgumentNullException("sitemapBuilder", "The parameter is null.");
            }

            _sitemapBuilder = sitemapBuilder;
        }

        #endregion Constructor.

        #region Methods.

        /// <summary>
        ///     Return the sitemap.
        /// </summary>
        /// <returns>XML content.</returns>
        public ActionResult Index()
        {
            return _GenerateSitemap(new SitemapService(Request.RequestContext, RouteTable.Routes));
        }

        #region Privates.

        /// <summary>
        ///     Generate a sitemap.
        /// </summary>
        /// <param name="service">Sitemap service.</param>
        /// <returns>XML content.</returns>
        private ContentResult _GenerateSitemap(SitemapService service)
        {
            return service.Generate(_sitemapBuilder, Request.RequestContext.HttpContext.Request.Url.GetHostNameWithProtocol());
        }

        #endregion Privates.

        #endregion Methods.
    }
}