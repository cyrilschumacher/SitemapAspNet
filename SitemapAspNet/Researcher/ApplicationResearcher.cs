using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SitemapAspNet.Researcher
{
    /// <summary>
    ///     Class search <see cref="Assembly"/> is an application.
    /// </summary>
    internal class ApplicationResearcher
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
        public ApplicationResearcher(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        #endregion Constructors section.

        #region Methods section.

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

        /// <summary>
        ///     Return a <see cref="Assembly"/> list represents a application.
        /// </summary>
        /// <returns>A <see cref="Assembly"/> list.</returns>
        public IEnumerable<Assembly> Search()
        {
            return _GetApplicationAssemblies();
        }

        #endregion Methods section.
    }
}