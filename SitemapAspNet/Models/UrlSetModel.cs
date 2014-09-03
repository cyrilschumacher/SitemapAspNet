﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace SitemapAspNet.Models
{
    /// <summary>
    ///     References the current protocol standard.
    /// </summary>
    /// <author>Cyril Schumacher</author>
    /// <date>22/08/2014T20:17:04+01:00</date>
    /// <copyright file="/Models/UrlSetModel.cs">
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