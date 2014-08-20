using System;
using System.Text;
using SitemapAspNet.Attributes;

namespace SitemapAspNet.Builders
{
    /// <summary>
    ///     Interface for sitemap builder.
    /// </summary>
    /// <author>Cyril Schumacher</author>
    /// <date>09/08/2014T13:18:32+01:00</date>
    internal interface ISitemapBuilder
    {
        /// <summary>
        ///     Create a URL entry.
        /// </summary>
        /// <param name="page">URL entry informations.</param>
        /// <param name="rootUri">Uri address of application.</param>
        void CreateEntry(SitemapAttribute page, Uri rootUri);

        /// <summary>
        ///     Generate a sitemap.
        /// </summary>
        string Generate();

        /// <summary>
        ///     Generate a sitemap.
        /// </summary>
        /// <param name="encoding">Encoding.</param>
        string Generate(Encoding encoding);
    }
}
