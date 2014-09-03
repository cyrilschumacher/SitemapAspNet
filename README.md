# Sitemap Generator for ASP.NET
Library for generating Sitemap for a ASP.NET website.
Available on [Nuget](https://www.nuget.org/packages/SitemapAspNet/).

## Requirement

- Microsoft .NET Framework 4.0 or higher.
- Application ASP.NET MVC 4 or higher.

## Example

### Configuration
To generate a site map, the library must be configured by providing the **RouteTable**. A **SitemapConfiguration** class provides a static method **Register**.
An example of use :

	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			// Provides routes to class configuration generator sitemap.
			SitemapConfiguration.Register(RouteTable.Routes);
		}
	}

### How to use
Après avoir configuré la librairie, il est possible d'utiliser l'attribut **SitemapAttribute** demandant des renseignements sur l'adresse.

Voici un exemple d'utilisation :

	public class CustomController : Controller
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