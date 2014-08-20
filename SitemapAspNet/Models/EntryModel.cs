using System;
using System.Xml.Serialization;

namespace SitemapAspNet.Models
{
    /// <summary>
    ///     Représente une entrée URL.
    /// </summary>
    [CLSCompliant(true)]
    [XmlRoot("url")]
    public class EntryModel
    {
        /// <summary>
        ///     Obtient ou définit une adresse.
        /// </summary>
        [XmlElement("loc")]
        public string Address { get; set; }

        /// <summary>
        ///     Obtient ou définit une date de modification.
        /// </summary>
        [XmlElement("lastmod")]
        public string LastModification { get; set; }

        /// <summary>
        ///     Obtient ou définit une fréquence de changement.
        /// </summary>
        [XmlElement("changefreq")]
        public string ChangeFrequently { get; set; }

        /// <summary>
        ///     Obtient ou définit une priorité.
        /// </summary>
        [XmlElement("priority")]
        public string Priority { get; set; }
    }
}