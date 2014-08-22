using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SitemapAspNet.Extensions
{
    /// <summary>
    ///     Extension class for <see cref="XmlSerializer" />.
    /// </summary>
    /// <author>Cyril Schumacher</author>
    /// <date>22/08/2014T20:33:21+01:00</date>
    /// <copyright file="/Extensions/XmlSerializerExtensions.cs">
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
    public static class XmlSerializerExtensions
    {
        #region Methods.

        /// <summary>
        ///     Serialize a object into <see cref="string" />.
        /// </summary>
        /// <typeparam name="T">Object type to serialize.</typeparam>
        /// <param name="model">Object to serialize.</param>
        /// <returns><see cref="string" /> represents object.</returns>
        public static string SerializeToString<T>(T model) where T : class
        {
            return SerializeToString(model, null, false);
        }

        /// <summary>
        ///     Serialize a object into <see cref="string" />.
        /// </summary>
        /// <typeparam name="T">Object type to serialize.</typeparam>
        /// <param name="model">Object to serialize.</param>
        /// <param name="encoding">Encoding.</param>
        /// <returns><see cref="string" /> represents object.</returns>
        public static string SerializeToString<T>(T model, Encoding encoding)
        {
            return SerializeToString(model, encoding, false);
        }

        /// <summary>
        ///     Serialize a object into <see cref="string" />.
        /// </summary>
        /// <typeparam name="T">Object type to serialize.</typeparam>
        /// <param name="model">Object to serialize.</param>
        /// <param name="encoding">Encoding.</param>
        /// <param name="indent">Must be intent.</param>
        /// <returns><see cref="string" /> represents object.</returns>
        public static string SerializeToString<T>(T model, Encoding encoding, bool indent)
        {
            using (var writer = new StringWriter(CultureInfo.InvariantCulture))
            {
                return _SerializeToString(new XmlSerializer(typeof (T)), model, writer, new XmlWriterSettings {Indent = indent, Encoding = encoding});
            }
        }

        #region Privates.

        /// <summary>
        ///     Serialize a object into <see cref="string" />.
        /// </summary>
        /// <param name="serializer"><see cref="XmlSerializer" /> instance.</param>
        /// <param name="model">Object to serialize.</param>
        /// <param name="writer">Stream contains result of the serialization.</param>
        /// <param name="settings">Parameters.</param>
        /// <returns><see cref="string" /> represents <paramref name="model" /> in XML format.</returns>
        private static string _SerializeToString<T>(XmlSerializer serializer, T model, TextWriter writer, XmlWriterSettings settings)
        {
            using (var xmlWriter = XmlWriter.Create(writer, settings))
            {
                serializer.Serialize(xmlWriter, model);
                return writer.ToString();
            }
        }

        #endregion Privates.

        #endregion Methods.
    }
}