using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace SitemapAspNet.Models
{
    /// <summary>
    ///     References the current protocol standard.
    /// </summary>
    [CLSCompliant(true)]
    [XmlRoot("urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class UrlSetModel
    {
        #region Properties.

        /// <summary>
        ///     Get or set a <see cref="EntryModel"/> list.
        /// </summary>
        /// <value><see cref="EntryModel"/> list.</value>
        [XmlElement("url")]
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"),
         SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<EntryModel> Url { get; set; }

        #endregion Properties.

        #region Constructor.

        /// <summary>
        ///     Constructor.
        /// </summary>
        public UrlSetModel()
        {
            Url = new List<EntryModel>();
        }

        #endregion Constructor.
    }
}