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
    [CLSCompliant(true)]
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap")]
    public class SitemapController : Controller
    {
        #region Members section.

        /// <summary>
        ///     Sitemap builder.
        /// </summary>
        private readonly ISitemapBuilder _sitemapBuilder;

        #endregion Members section.

        #region Constructors section.

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <seealso cref="SitemapController(ISitemapBuilder)" />
        public SitemapController()
            : this(new XmlSitemapBuilder())
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="builder">Sitemap builder.</param>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="builder" /> is null.</exception>
        public SitemapController(ISitemapBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder", "Value cannot be null.");
            }

            _sitemapBuilder = builder;
        }

        #endregion Constructors section.

        #region Methods section.

        #region Privates.

        /// <summary>
        ///     Generate a sitemap.
        /// </summary>
        /// <param name="service">Sitemap service.</param>
        /// <returns>XML content.</returns>
        private ContentResult _GenerateSitemap(SitemapService service)
        {
            return service.Generate(_sitemapBuilder, Request.Url.GetHostNameWithProtocol());
        }

        #endregion Privates.

        /// <summary>
        ///     Return the sitemap.
        /// </summary>
        /// <returns>XML content.</returns>
        public ActionResult Index()
        {
            return _GenerateSitemap(new SitemapService(Request.RequestContext, RouteTable.Routes));
        }

        #endregion Methods section.
    }
}