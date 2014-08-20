using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

// Conformité CLS.
[assembly: CLSCompliant(true)]

// Les informations générales relatives à un assembly dépendent de
// l'ensemble d'attributs suivant.
[assembly: AssemblyTitle("SitemapAspNet")]
[assembly: AssemblyDescription("Library to generate a sitemap ASP.NET.")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyProduct("SitemapAspNet")]
[assembly: AssemblyCopyright("")]

// Visibilité de l'assembly depuis des composants COM.
[assembly: ComVisible(false)]

// Le GUID suivant est pour l'ID de la typelib si ce projet est exposé à COM
[assembly: Guid("88f3e204-89d1-449d-a6b3-800b5c6c83fb")]

// Les informations de version pour un assembly se composent des quatre valeurs suivantes :
// Version principale, Version secondaire, Numéro de build, Révision
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: NeutralResourcesLanguageAttribute("fr-fr")]