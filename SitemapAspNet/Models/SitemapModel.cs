using SitemapAspNet.Attributes;
using System;
using System.Diagnostics.CodeAnalysis;

namespace SitemapAspNet.Models
{
    /// <summary>
    ///     Page informations.
    /// </summary>
    [CLSCompliant(true)]
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap")]
    public class SitemapModel
    {
        #region Properties section.

        /// <summary>
        ///     Get or set a controller name.
        /// </summary>
        /// <value>Controller name.</value>
        public string ControllerName { get; set; }

        /// <summary>
        ///     Get or set action name.
        /// </summary>
        /// <value>Action name.</value>
        public string ActionName { get; set; }

        /// <summary>
        ///     Get or set a <see cref="SitemapAttribute" /> attribute.
        /// </summary>
        /// <value><see cref="SitemapAttribute" /> attribute.</value>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap")]
        public SitemapAttribute Sitemap { get; set; }

        #endregion Properties section.
    }
}