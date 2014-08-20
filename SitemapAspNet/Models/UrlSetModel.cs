using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace SitemapAspNet.Models
{
    /// <summary>
    ///     Représente un plan de site.
    /// </summary>
    [CLSCompliant(true)]
    [XmlRoot("urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class UrlSetModel
    {
        /// <summary>
        ///     Représente des entrées URL.
        /// </summary>
        [XmlElement("url")]
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"),
         SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<EntryModel> Url { get; set; }

        /// <summary>
        ///     Constructeur.
        /// </summary>
        public UrlSetModel()
        {
            Url = new List<EntryModel>();
        }
    }
}