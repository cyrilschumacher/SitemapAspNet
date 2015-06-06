# Sitemap Generator for ASP.NET MVC
## What's it?
This is a library for generating Sitemap for a ASP.NET MVC website. Available on [Nuget](https://www.nuget.org/packages/SitemapAspNet/) and a documentation is available [here](http://cyrilschumacher.github.io/SitemapAspNet/).

## What are the requirement?
- Microsoft .NET Framework 4.0 or higher.
- Application ASP.NET MVC 4 or higher.

## How do I set it up?
To generate a site map, the library must be configured by providing the **RouteTable**. A **SitemapConfiguration** class provides a static method **Register**.
An example of use :

> ```csharp
> public class MvcApplication : HttpApplication
> {
>     protected void Application_Start()
>     {
>         // Provides routes to class configuration generator sitemap.
>         SitemapConfiguration.Register(RouteTable.Routes);
>     }
> }
> ```

## How to use?
After configuring the library, it's possible to use **SitemapAttribute** attribute for to include a Web page.
An example of use :

> ```csharp
> public class CustomController : Controller
> {
>     // Create a URL entry with a modification date, frequency and priority.
>	  [Sitemap("2014-08-20", SitemapAttribute.Frequence.Monthly, 0.7D)]
>	  public ViewResult Index()
>	  {
>	      return View();
>	  }
>
>	  // Create a URL entry with only a modification date.
>	  [Sitemap("2014-08-19")]
>	  public ActionResult Contact()
>	  {
>	      return View();
>	  }
> }
> ```


## Copyright

> The MIT License (MIT)
> 
> Copyright (c) 2015 Cyril Schumacher.fr
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy
> of this software and associated documentation files (the "Software"), to deal
> in the Software without restriction, including without limitation the rights
> to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
> copies of the Software, and to permit persons to whom the Software is
> furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all
> copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
> IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
> FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
> AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
> LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
> OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
> SOFTWARE.