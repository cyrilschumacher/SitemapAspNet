using SitemapAspNet.Attributes;
using SitemapAspNet.Extensions;
using SitemapAspNet.Models;
using System;
using System.Text;

namespace SitemapAspNet.Builders
{
    /// <summary>
    ///     Sitemap builder.
    /// </summary>
    /// <author>Cyril Schumacher</author>
    /// <date>09/08/2014T13:18:14+01:00</date>
    internal class SitemapBuilder : ISitemapBuilder
    {
        #region Fields.

        /// <summary>
        ///     Model.
        /// </summary>
        private readonly UrlSetModel _model;

        #endregion Fields.

        #region Constructor.

        /// <summary>
        ///     Constructor.
        /// </summary>
        public SitemapBuilder()
        {
            _model = new UrlSetModel();
        }

        #endregion Constructor.

        #region Methods.

        /// <summary>
        ///     Create a URL entry.
        /// </summary>
        /// <param name="page">URL entry informations.</param>
        /// <param name="rootUri">Uri address of application.</param>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="page" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="rootUri" /> is null.</exception>
        public void CreateEntry(SitemapAttribute page, Uri rootUri)
        {
            if (page == null) throw new ArgumentNullException("page", "The parameter is null.");
            if (rootUri == null) throw new ArgumentNullException("rootUri", "The parameter is null.");

            _model.Url.Add(new EntryModel
            {
                Address = new Uri(rootUri, page.Address).OriginalString,
                ChangeFrequently = page.ChangeFrequently,
                Priority = page.Priority,
                LastModification = page.LastModification
            });
        }

        /// <summary>
        ///     Generate a sitemap.
        /// </summary>
        /// <seealso cref="Generate(Encoding)"/>
        public string Generate()
        {
            return Generate(Encoding.UTF8);
        }

        /// <summary>
        ///     Generate a sitemap.
        /// </summary>
        /// <param name="encoding">Encoding.</param>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="encoding" /> is null.</exception>
        public string Generate(Encoding encoding)
        {
            if (encoding == null) throw new ArgumentNullException("encoding", "The parameter is null.");
            return XmlSerializerExtensions.SerializeToString(_model, encoding);
        }

        #endregion Methods.
    }
}