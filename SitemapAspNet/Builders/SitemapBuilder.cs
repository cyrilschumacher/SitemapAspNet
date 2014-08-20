using SitemapAspNet.Attributes;
using SitemapAspNet.Extensions;
using SitemapAspNet.Models;
using System;
using System.Text;

namespace SitemapAspNet.Builders
{
    /// <summary>
    ///     Monteur de plan de site.
    /// </summary>
    /// <copyright file="/Builders/SitemapBuilder.cs">
    ///     Copyright (c) 2014 Cyril Schumacher.fr All Rights Reserved
    /// </copyright>
    /// <author>Cyril Schumacher</author>
    /// <date>09/08/2014T13:18:14+01:00</date>
    internal class SitemapBuilder : ISitemapBuilder
    {
        #region Membres.

        /// <summary>
        ///     Model.
        /// </summary>
        private readonly UrlSetModel _model;

        #endregion Membres.

        #region Constructeur.

        /// <summary>
        ///     Constructeur.
        /// </summary>
        public SitemapBuilder()
        {
            _model = new UrlSetModel();
        }

        #endregion Constructeur.

        #region Méthodes.

        /// <summary>
        ///     Créer une entrée URL.
        /// </summary>
        /// <param name="page">Information de la page.</param>
        /// <param name="rootUri">Adresse absolue de l'application.</param>
        /// <exception cref="ArgumentNullException">Se lève si <paramref name="page" /> a la valeur null.</exception>
        /// <exception cref="ArgumentNullException">Se lève si <paramref name="rootUri" /> a la valeur null.</exception>
        public void CreateEntry(SitemapAttribute page, Uri rootUri)
        {
            if (page == null) throw new ArgumentNullException("page");
            if (rootUri == null) throw new ArgumentNullException("rootUri");

            _model.Url.Add(new EntryModel
            {
                Address = new Uri(rootUri, page.Address).OriginalString,
                ChangeFrequently = page.ChangeFrequently,
                Priority = page.Priority,
                LastModification = page.LastModification
            });
        }

        /// <summary>
        ///     Génére un plan de site.
        /// </summary>
        /// <param name="encoding">Encodage.</param>
        public string Generate(Encoding encoding = null)
        {
            return XmlSerializerExtensions.SerializeToString(_model, encoding ?? Encoding.UTF8);
        }

        #endregion Méthodes.
    }
}