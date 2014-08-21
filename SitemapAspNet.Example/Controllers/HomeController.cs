using System.Web.Mvc;
using SitemapAspNet.Attributes;

namespace SitemapAspNet.Example.Controllers
{
    public class HomeController : Controller
    {
        // Create a URL entry with a modification date, frequency and priority.
        [Sitemap("2014-08-20", SitemapAttribute.Frequence.Monthly, 0.7D)]
        public ViewResult Index()
        {
            return View();
        }

        // Create a URL entry with only a modification date.
        [Sitemap("2014-08-19")]
        public ActionResult Contact()
        {
            return View();
        }
    }
}