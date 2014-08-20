using System.Web.Mvc;
using SitemapAspNet.Attributes;

namespace SitemapAspNet.Example.Controllers
{
    public class HomeController : Controller
    {
        [Sitemap("2014-08-20", SitemapAttribute.Frequence.Monthly, 0.7D)]
        public ViewResult Index()
        {
            return View();
        }

        [Sitemap("2014-08-19")]
        public ActionResult Contact()
        {
            return View();
        }
    }
}