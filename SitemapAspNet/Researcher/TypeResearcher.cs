using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SitemapAspNet.Researcher
{
    /// <summary>
    ///     Class search <see cref="Type" />.
    /// </summary>
    internal sealed class TypeResearcher
    {
        #region Members section.

        /// <summary>
        ///     <see cref="Assembly"/> list.
        /// </summary>
        private readonly IEnumerable<Assembly> _assemblies;

        #endregion Members section.

        #region Constructors section.

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="assemblies"><see cref="Assembly"/> list.</param>
        public TypeResearcher(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        #endregion Constructors section.

        #region Methods section.

        #region Privates.

        /// <summary>
        ///     Determines if a <paramref name="type"/> is <paramref name="desiredType"/>
        /// </summary>
        /// <param name="desiredType">The desired type.</param>
        /// <param name="type">The type to check.</param>
        /// <returns>True if type is, False else.</returns>
        private static bool _IsTypeRequired(Type desiredType, Type type)
        {
            return type.IsSubclassOf(desiredType);
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

        #endregion Privates.

        /// <summary>
        ///     Search <see cref="Type" /> by a <see cref="Type" />.
        /// </summary>
        /// <param name="desiredType">The <see cref="Type" /> searched.</param>
        /// <returns>The <see cref="Type" /> list.</returns>
        public IEnumerable<Type> Search(Type desiredType)
        {
            return _assemblies.SelectMany(_GetTypesAssemblies).Where(type => _IsTypeRequired(type, desiredType));
        }

        #endregion Methods section.
    }
}