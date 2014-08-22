using System;
using System.Xml.Serialization;

namespace SitemapAspNet.Models
{
    /// <summary>
    ///     Entry URL.
    /// </summary>
    /// <author>Cyril Schumacher</author>
    /// <date>22/08/2014T20:17:04+01:00</date>
    /// <copyright file="/Models/EntryModel.cs">
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