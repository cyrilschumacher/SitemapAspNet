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
        ///     Retourne une liste <see cref="Assembly"/> représentant une application.
        /// </summary>
        /// <returns>Liste <see cref="Assembly"/> représentant une application.</returns>
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