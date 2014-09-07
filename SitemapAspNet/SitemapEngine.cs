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
    /// <copyright file="/SitemapEngine.cs">
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
    internal sealed class SitemapEngine
    {
        #region Fields.

        /// <summary>
        ///     Controllers.
        /// </summary>
        private readonly IEnumerable<Type> _controllers;

        #endregion Fields.

        #region Constructor.

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="controllers">Controllers type.</param>
        public SitemapEngine(IEnumerable<Type> controllers)
        {
            _controllers = controllers;
        }

        #endregion Constructor.

        #region Methods.

        /// <summary>
        ///     Return pages.
        /// </summary>
        /// <returns>Pages.</returns>
        public IEnumerable<SitemapModel> GetPages()
        {
            return _GetSitemapAttributes(new List<SitemapModel>());
        }

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

        #endregion Methods.
    }
}