using System;
using System.Text;
using SitemapAspNet.Attributes;

namespace SitemapAspNet.Builders
{
    /// <summary>
    ///     Interface pour un monteur de plan de site.
    /// </summary>
    /// <author>Cyril Schumacher</author>
    /// <date>09/08/2014T13:18:32+01:00</date>
    internal interface ISitemapBuilder
    {
        /// <summary>
        ///     Créer une entrée URL.
        /// </summary>
        /// <param name="page">Information de la page.</param>
        /// <param name="rootUri">Adresse absolue de l'application.</param>
        void CreateEntry(SitemapAttribute page, Uri rootUri);

        /// <summary>
        ///     Génére un plan de site.
        /// </summary>
        /// <param name="encoding">Encodage.</param>
        string Generate(Encoding encoding = null);
    }
}
