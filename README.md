# Sitemap Generator for ASP.NET # 
Librairie de génération de plan de site pour un site web ASP.NET.

## Configuration requise ##

- Microsoft .NET Framework 4.0
- Application ASP.NET MVC

## Exemple ##

### Configuration ###

Pour que la librairie génére un plan de site, celle-ci requiert les itinéraires d'URL de l'application. Pour cela, une classe **SitemapConfiguration** propose une méthode statique **Register** demandant les itinéraires. 

Voici un exemple d'utilisation :

	public class MvcApplication : HttpApplication
	{
        protected void Application_Start()
		{
			// Provides routes to class configuration generator sitemap.
    		SitemapConfiguration.Register(RouteTable.Routes);
		}
	}

### Utilisation ###

Après avoir configuré la librairie, il est possible d'utiliser l'attribut **SitemapAttribute** demandant des renseignements sur l'adresse.

Voici un exemple d'utilisation :

	public class CustomController : Controller
	{
		[Sitemap("2014-08-20", SitemapAttribute.Frequence.Monthly, 0.7D)]
		public ViewResult Index()
		{
			return View();
		}
	}