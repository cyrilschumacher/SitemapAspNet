using SitemapAspNet.Attributes;
using SitemapAspNet.Extensions;
using SitemapAspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SitemapAspNet
{
    /// <summary>
    ///     Sitemap engine.
    /// </summary>
    /// <author>Cyril Schumacher</author>
    /// <date>15/08/2014T11:26:41+01:00</date>
    internal sealed class SitemapEngine
    {
        #region Membres.

        /// <summary>
        ///     Controllers.
        /// </summary>
        private readonly IEnumerable<Type> _controllers;

        #endregion Membres.

        #region Constructeur.

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="controllers">Contrôleurs.</param>
        public SitemapEngine(IEnumerable<Type> controllers)
        {
            _controllers = controllers;
        }

        #endregion Constructeur.

        #region Méthodes.

        #region Privées.

        /// <summary>
        ///     Obtient les informations sur les pages suivants les contrôleurs existants.
        /// </summary>
        /// <returns>Informations sur les pages.</returns>
        private IEnumerable<SitemapModel> _GetSitemapAttributes(IEnumerable<SitemapModel> controllers)
        {
            // Obtient les informations sur les pages suivants les contrôleurs existants.
            return _controllers.Select(_GetControllersInformations).Aggregate(controllers, Enumerable.Concat);
        }

        /// <summary>
        ///     Return <see cref="SitemapAttribute" /> of method.
        /// </summary>
        /// <param name="methodInfo">Method informations.</param>
        /// <returns><see cref="SitemapAttribute"/>.</returns>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="methodInfo" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throw if <see cref="MemberInfo.ReflectedType"/> of <paramref name="methodInfo" /> is null.</exception>
        private static SitemapModel _GetAttributeMethod(MemberInfo methodInfo)
        {
            if ((methodInfo == null) || (methodInfo.ReflectedType == null))
            {
                throw new ArgumentNullException("methodInfo", "The parameter is null.");
            }
            if (methodInfo.ReflectedType == null)
            {
                throw new ArgumentOutOfRangeException("methodInfo", methodInfo.ReflectedType, "The reflected type property is null.");
            }

            return new SitemapModel
            {
                ControllerName = methodInfo.ReflectedType.Name,
                ActionName = methodInfo.Name,
                Sitemap = methodInfo.GetCustomAttributesByType<SitemapAttribute>().FirstOrDefault()
            };
        }

        /// <summary>
        ///     Retourne les pages dites "valides".
        /// </summary>
        /// <param name="pages">Pages.</param>
        /// <returns>Liste de <see cref="SitemapModel"/> valide.</returns>
        private static IEnumerable<SitemapModel> _GetValidPages(IEnumerable<SitemapModel> pages)
        {
            return from page in pages where (page.Sitemap != null) select page;
        }

        /// <summary>
        ///     Retourne les informations sur les pages HTML.
        /// </summary>
        /// <param name="typeController">Type du contrôleur.</param>
        /// <returns>Une liste des informations des pages.</returns>
        private static IEnumerable<SitemapModel> _GetControllersInformations(Type typeController)
        {
            var attributes = from attribute in typeController.GetMethods()
                             where (attribute != null)
                             select _GetAttributeMethod(attribute);

            return _GetValidPages(attributes);
        }

        #endregion Privées.

        /// <summary>
        ///     Retourne les pages.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SitemapModel> GetPages()
        {
            return _GetSitemapAttributes(new List<SitemapModel>());
        }

        #endregion Méthodes.
    }
}