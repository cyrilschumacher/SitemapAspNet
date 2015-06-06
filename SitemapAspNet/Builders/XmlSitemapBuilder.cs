using System;
using System.Text;
using SitemapAspNet.Attributes;
using SitemapAspNet.Extensions;
using SitemapAspNet.Models;

namespace SitemapAspNet.Builders
{
    /// <summary>
    ///     XML Sitemap builder.
    /// </summary>
    internal class XmlSitemapBuilder : ISitemapBuilder
    {
        #region Members section.

        /// <summary>
        ///     Model.
        /// </summary>
        private readonly UrlSetModel _model;

        #endregion Members section.

        #region Constructors section.

        /// <summary>
        ///     Constructor.
        /// </summary>
        public XmlSitemapBuilder()
            : this(new UrlSetModel())
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="model">Model.</param>
        public XmlSitemapBuilder(UrlSetModel model)
        {
            _model = model;
        }

        #endregion Constructors section.

        #region Methods section.

        #region Privates.

        /// <summary>
        ///     Add a entry.
        /// </summary>
        /// <param name="entry">Entry.</param>
        private void _AddEntry(EntryModel entry)
        {
            if (!_model.Url.Contains(entry))
            {
                _model.Url.Add(entry);
            }
        }

        /// <summary>
        ///     Create a URL entry.
        /// </summary>
        /// <param name="address">Uri address.</param>
        /// <param name="changeFrequentyle">Frequency.</param>
        /// <param name="priority">Priorirty.</param>
        /// <param name="lastModification">Last modification file.</param>
        /// <returns>URL entry.</returns>
        private static EntryModel _CreateEntry(string address, string changeFrequentyle, string priority,
            string lastModification)
        {
            return new EntryModel
            {
                Address = address,
                ChangeFrequently = changeFrequentyle,
                Priority = priority,
                LastModification = lastModification
            };
        }

        #endregion Privates.

        /// <summary>
        ///     Create a URL entry.
        /// </summary>
        /// <param name="page">URL entry informations.</param>
        /// <param name="rootUri">Uri address of application.</param>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="page" /> or <paramref name="rootUri" /> are null.</exception>
        public void CreateEntry(SitemapAttribute page, Uri rootUri)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }
            if (rootUri == null)
            {
                throw new ArgumentNullException("rootUri");
            }

            var entry = _CreateEntry(new Uri(rootUri, page.Address).OriginalString, page.ChangeFrequently, page.Priority,
                page.LastModification);
            _AddEntry(entry);
        }

        /// <summary>
        ///     Generate a sitemap.
        /// </summary>
        /// <seealso cref="Generate(Encoding)" />
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
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            return XmlSerializerExtensions.SerializeToString(_model, encoding);
        }

        #endregion Methods section.
    }
}