using System;
using System.Xml.Serialization;

namespace SitemapAspNet.Models
{
    /// <summary>
    ///     Entry URL.
    /// </summary>
    [CLSCompliant(true)]
    [XmlRoot("url")]
    public class EntryModel
    {
        #region Properties.

        /// <summary>
        ///     Get or set a Uri address.
        /// </summary>
        /// <value>Uri address.</value>
        [XmlElement("loc")]
        public string Address { get; set; }

        /// <summary>
        ///     Get or set a last date modification.
        /// </summary>
        /// <value>Last date modification.</value>
        [XmlElement("lastmod")]
        public string LastModification { get; set; }

        /// <summary>
        ///     Get or set a frequency.
        /// </summary>
        /// <value>Frequency.</value>
        [XmlElement("changefreq")]
        public string ChangeFrequently { get; set; }

        /// <summary>
        ///     Get or set a priority.
        /// </summary>
        /// <value>Priority.</value>
        [XmlElement("priority")]
        public string Priority { get; set; }

        #endregion Properties.
    }
}