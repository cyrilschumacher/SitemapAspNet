using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using System.Web.Routing;
using SitemapAspNet.Builders;
using SitemapAspNet.Extensions;

namespace SitemapAspNet.Controllers
{
    /// <summary>
    ///     Sitemap Controller.
    /// </summary>
    /// <author>Cyril Schumacher</author>
    /// <date>08/08/2014T21:17:52+01:00</date>
    [CLSCompliant(true)]
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap")]
    public class SitemapController : Controller
    {
        #region Méthodes.

        #region Privées.

        /// <summary>
        ///     Generate a sitemap.
        /// </summary>
        /// <param name="service">Sitemap service.</param>
        /// <returns>XML content.</returns>
        private ContentResult _GenerateSitemap(SitemapService service)
        {
            return service.Generate(new SitemapBuilder(),
                Request.RequestContext.HttpContext.Request.Url.GetHostNameWithProtocol());
        }

        #endregion

        /// <summary>
        ///     Return the sitemap.
        /// </summary>
        /// <returns>XML content.</returns>
        public ActionResult Index()
        {
            return _GenerateSitemap(new SitemapService(Request.RequestContext, RouteTable.Routes));
        }

        #endregion
    }
}