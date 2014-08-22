using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SitemapAspNet.Researcher
{
    /// <summary>
    ///     Class search <see cref="Type" />.
    /// </summary>
    /// <author>Cyril Schumacher</author>
    /// <date>15/08/2014T11:26:41+01:00</date>
    /// <copyright file="/Researcher/TypeResearcher.cs">
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
    internal sealed class TypeResearcher
    {
        #region Fields.

        /// <summary>
        ///     <see cref="Assembly"/> list.
        /// </summary>
        private readonly IEnumerable<Assembly> _assemblies;

        /// <summary>
        ///     Type desired.
        /// </summary>
        private Type _type;

        #endregion Fields.

        #region Constructor.

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="assemblies"><see cref="Assembly"/> list.</param>
        public TypeResearcher(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        #endregion Constructor.

        #region Methods.

        /// <summary>
        ///     Search <see cref="Type" /> by a <see cref="Type" />.
        /// </summary>
        /// <param name="type"><see cref="Type" /> searched.</param>
        /// <returns><see cref="Type" /> list.</returns>
        public IEnumerable<Type> Search(Type type)
        {
            _type = type;
            return _GetClassTypes();
        }

        #region Privates.

        /// <summary>
        ///     Determines if a <see cref="Type"/> is <see cref="_type"/>.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>True if type is, False else.</returns>
        private bool _IsTypeRequired(Type type)
        {
            return type.IsSubclassOf(_type);
        }

        /// <summary>
        ///     Return types corresponding to a <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        /// <returns><see cref="Assembly"/> list.</returns>
        private static IEnumerable<Type> _GetTypesAssemblies(Assembly assembly)
        {
            return assembly.GetTypes();
        }

        /// <summary>
        ///     Return for the types class.
        /// </summary>
        /// <returns>Types class.</returns>
        private IEnumerable<Type> _GetClassTypes()
        {
            return _assemblies.SelectMany(_GetTypesAssemblies).Where(_IsTypeRequired);
        }

        #endregion Privates.

        #endregion Methods.
    }
}