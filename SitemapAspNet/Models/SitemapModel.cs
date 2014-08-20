using System;
using System.Diagnostics.CodeAnalysis;
using SitemapAspNet.Attributes;

namespace SitemapAspNet.Models
{
    /// <summary>
    ///     Réprésentation d'une page.
    /// </summary>
    [CLSCompliant(true)]
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap")]
    public class SitemapModel
    {
        /// <summary>
        ///     Obtient ou définit un nom de contrôleur.
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        ///     Obtient ou définit un nom d'action.
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        ///     Obtient ou définit un attribut <see cref="SitemapAttribute" />.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap")]
        public SitemapAttribute Sitemap { get; set; }
    }
}