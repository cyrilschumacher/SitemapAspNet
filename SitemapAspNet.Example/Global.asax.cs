using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SitemapAspNet.Configurations;

namespace SitemapAspNet.Example
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Provides routes to class configuration generator sitemap.
            SitemapConfiguration.Register(RouteTable.Routes);
        }
    }
}
