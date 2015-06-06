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
    internal sealed class SitemapEngine
    {
        #region Members section.

        /// <summary>
        ///     Controllers.
        /// </summary>
        private readonly IEnumerable<Type> _controllers;

        #endregion Members section.

        #region Constructor section.

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="controllers">Controllers type.</param>
        public SitemapEngine(IEnumerable<Type> controllers)
        {
            _controllers = controllers;
        }

        #endregion Constructor section.

        #region Methods section.

        #region Privates.

        /// <summary>
        ///     Return pages informations following existing controller.
        /// </summary>
        /// <returns>Informations sur les pages.</returns>
        private IEnumerable<SitemapModel> _GetSitemapAttributes(IEnumerable<SitemapModel> controllers)
        {
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
                throw new ArgumentNullException("methodInfo", "Value cannot be null.");
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
        ///     Return valid pages.
        /// </summary>
        /// <param name="pages">Pages.</param>
        /// <returns><see cref="SitemapModel"/> list valid.</returns>
        private static IEnumerable<SitemapModel> _GetValidPages(IEnumerable<SitemapModel> pages)
        {
            return from page in pages where (page.Sitemap != null) select page;
        }

        /// <summary>
        ///     Return informations pages.
        /// </summary>
        /// <param name="typeController">Controller type.</param>
        /// <returns>Pages informations list.</returns>
        private static IEnumerable<SitemapModel> _GetControllersInformations(Type typeController)
        {
            var attributes = from attribute in typeController.GetMethods()
                             where (attribute != null)
                             select _GetAttributeMethod(attribute);

            return _GetValidPages(attributes);
        }

        #endregion Privates.

        /// <summary>
        ///     Return pages.
        /// </summary>
        /// <returns>Pages.</returns>
        public IEnumerable<SitemapModel> GetPages()
        {
            return _GetSitemapAttributes(new List<SitemapModel>());
        }

        #endregion Methods section.
    }
}