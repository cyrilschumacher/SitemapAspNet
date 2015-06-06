using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

// Conformité CLS.
[assembly: CLSCompliant(true)]

// General Information about an assembly is controlled of 
// the following set of attributes.
[assembly: AssemblyTitle("SitemapAspNet")]
[assembly: AssemblyDescription("Library to generate a sitemap ASP.NET.")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyProduct("SitemapAspNet")]
[assembly: AssemblyCopyright("Copyright (c) 2015 Cyril Schumacher.fr")]

// Visibility assembly from COM components.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM.
[assembly: Guid("88f3e204-89d1-449d-a6b3-800b5c6c83fb")]

// Version information for an assembly consists of the following four values : 
// Major Version, Minor Version, Build Number, Revision
[assembly: AssemblyVersion("1.2.1")]
[assembly: AssemblyFileVersion("1.2.1.0")]

// Suppress messages from the Microsoft analysis.
[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "SitemapAspNet.Configurations")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap", Scope = "namespace", Target = "SitemapAspNet.Configurations")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap", Scope = "namespace", Target = "SitemapAspNet.Models")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap", Scope = "namespace", Target = "SitemapAspNet.Attributes")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap", Scope = "namespace", Target = "SitemapAspNet.Extensions")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap", Scope = "namespace", Target = "SitemapAspNet.Controllers")]