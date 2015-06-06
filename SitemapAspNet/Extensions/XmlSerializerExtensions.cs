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
    [CLSCompliant(true)]
    public static class XmlSerializerExtensions
    {
        #region Methods section.

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
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            using (var writer = new StringWriter(CultureInfo.InvariantCulture))
            {
                return _SerializeToString(new XmlSerializer(typeof (T)), model, writer, new XmlWriterSettings {Indent = indent, Encoding = encoding});
            }
        }

        #endregion Methods section.
    }
}