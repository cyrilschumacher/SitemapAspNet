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
        ///     Recherche des <see cref="Type" /> selon un <see cref="Type" />.
        /// </summary>
        /// <param name="type"><see cref="Type" /> recherché.</param>
        /// <returns>Liste <see cref="Type" />.</returns>
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