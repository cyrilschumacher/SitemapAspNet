using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SitemapAspNet.Researcher
{
    /// <summary>
    ///     Class search <see cref="Assembly"/> is an application.
    /// </summary>
    /// <author>Cyril Schumacher</author>
    /// <date>15/08/2014T11:26:41+01:00</date>
    /// <copyright file="/Researcher/ApplicationResearcher.cs">
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
    internal class ApplicationResearcher
    {
        #region Fields.

        /// <summary>
        ///     <see cref="Assembly"/> list.
        /// </summary>
        private readonly IEnumerable<Assembly> _assemblies;

        #endregion Fields.

        #region Constructor.

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="assemblies"><see cref="Assembly"/> list.</param>
        public ApplicationResearcher(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        #endregion Constructor.

        #region Methods.

        /// <summary>
        ///     Return a <see cref="Assembly"/> list represents a application.
        /// </summary>
        /// <returns>A <see cref="Assembly"/> list.</returns>
        public IEnumerable<Assembly> Search()
        {
            return _GetApplicationAssemblies();
        }

        #region Privates.

        /// <summary>
        ///     Return application assembly.
        /// </summary>
        /// <returns>Assemblies.</returns>
        private IEnumerable<Assembly> _GetApplicationAssemblies()
        {
            return _assemblies.Where(_IsApplication);
        }

        /// <summary>
        ///     Determines if the assembly represent a application.
        /// </summary>
        /// <param name="assembly">Assembly to test.</param>
        /// <returns>True if assembly represents a application, False else.</returns>
        private static bool _IsApplication(Assembly assembly)
        {
            return !assembly.GlobalAssemblyCache && !assembly.ReflectionOnly;
        }

        #endregion Privates.

        #endregion Methods.
    }
}