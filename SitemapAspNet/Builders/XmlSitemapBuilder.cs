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
    /// <author>Cyril Schumacher</author>
    /// <date>09/08/2014T13:18:14+01:00</date>
    /// <copyright file="/Builders/XmlSitemapBuilder.cs">
    ///     The MIT License (MIT)
    ///
    ///     Copyright (c) 2014, SitemapAspNet by Cyril Schumacher
    ///
    ///     Permission is hereby granted, free of charge, to any person obtaining a copy
    ///     of this software and associated documentation files (the "Software"), to deal
    ///     in the Software without restriction, including without limitation the rights
    ///     to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    ///     copies of the Software, and to permit persons to whom the Software is
    ///     furnished to do so, subject to the following conditions:
    ///
    ///     The above copyright notice and this permission notice shall be included in
    ///     all copies or substantial portions of the Software.
    ///
    ///     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    ///     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    ///     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    ///     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    ///     LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    ///     OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    ///     THE SOFTWARE.
    /// </copyright>
    internal class XmlSitemapBuilder : ISitemapBuilder
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

        #endregion Constructor.

        #region Methods.

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
                throw new ArgumentNullException("page", "Value cannot be null.");
            }
            if (rootUri == null)
            {
                throw new ArgumentNullException("rootUri", "Value cannot be null.");
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
                throw new ArgumentNullException("encoding", "Value cannot be null.");
            }

            return XmlSerializerExtensions.SerializeToString(_model, encoding);
        }

        #region Privates.

        /// <summary>
        ///     Add a entry.
        /// </summary>
        /// <param name="entry">Entry.</param>
        private void _AddEntry(EntryModel entry)
        {
            if (_model.Url.Contains(entry))
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

        #endregion Methods.
    }
}