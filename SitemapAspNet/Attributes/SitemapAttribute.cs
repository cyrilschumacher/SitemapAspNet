using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.Mvc;

namespace SitemapAspNet.Attributes
{
    /// <summary>
    ///     Attribute for <see cref="ActionResult" /> method.
    /// </summary>
    /// <author>Cyril Schumacher</author>
    /// <date>15/02/2014T18:18:16+01:00</date>
    [CLSCompliant(true), AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap"),
     SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    public sealed class SitemapAttribute : Attribute
    {
        #region Constants.

        /// <summary>
        ///     Default priority of a page.
        ///     Priorité par défaut d'une page.
        /// </summary>
        private const double PriorityDefault = 0.5;

        #endregion Constants.

        #region Fields.

        /// <summary>
        ///     Frequency of the page.
        /// </summary>
        private readonly string _changeFrequently;

        /// <summary>
        ///     Date of last modification file.
        /// </summary>
        private readonly string _lastModification;

        /// <summary>
        ///     Priority of URL entry.
        /// </summary>
        private readonly string _priority;

        #endregion Fields.

        #region Properties.

        /// <summary>
        ///     Get or set a URL entry.
        /// </summary>
        /// <value>URL entry.</value>
        public string Address { get; set; }

        /// <summary>
        ///     Get a frequency of the page.
        /// </summary>
        /// <value>Frequency.</value>
        /// <seealso cref="Frequence" />
        public string ChangeFrequently
        {
            get { return _changeFrequently; }
        }

        /// <summary>
        ///     Get a date of last modification file.
        /// </summary>
        /// <value>Date of last modification file.</value>
        /// <remarks>The date must be in the format W3C: YYYY-MM-DDThh: mm: ss.sTZD.</remarks>
        public string LastModification
        {
            get { return _lastModification; }
        }

        /// <summary>
        ///     Get a priority of URL entry.
        /// </summary>
        /// <value>Priority of URL entry.</value>
        /// <remarks>The priority must be represented by float number between 0.0 and 1.0.</remarks>
        public string Priority
        {
            get { return _priority; }
        }

        #endregion Properties.

        #region Constructeur.

        /// <summary>
        ///     Constructor.
        /// </summary>
        public SitemapAttribute()
            : this(null, Frequence.Never, PriorityDefault)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="lastModification">Date of last modification file.</param>
        public SitemapAttribute(string lastModification)
            : this(lastModification, Frequence.Never, PriorityDefault)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="lastModification">Date of last modification file.</param>
        /// <param name="changeFrequently">Frequency of the page.</param>
        public SitemapAttribute(string lastModification, Frequence changeFrequently)
            : this(lastModification, changeFrequently, PriorityDefault)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="lastModification">Optional. Date of last modification file.</param>
        /// <param name="changeFrequently">Optional. Frequency of the page.</param>
        /// <param name="priority">Optional. Priority of URL entry.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public SitemapAttribute(string lastModification, Frequence changeFrequently, double priority)
        {
            if ((priority < 0.0) && (1.0 > priority))
            {
                throw new ArgumentOutOfRangeException("priority", priority, "Priority must be between 0.0 and 1.0.");
            }

            if (!IsValidDate(lastModification))
            {
                throw new ArgumentException("The date isn't in the W3C format.", "lastModification");
            }

            if (!changeFrequently.Equals(Frequence.Never))
            {
                _changeFrequently = changeFrequently.ToString().ToLowerInvariant();
            }

            _lastModification = lastModification;
            _priority = priority.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="lastModification">Date of last modification file.</param>
        /// <param name="changeFrequently">Frequency of the page.</param>
        /// <param name="priority">Priority of URL entry.</param>
        internal SitemapAttribute(string lastModification, string changeFrequently, string priority)
        {
            _lastModification = lastModification;
            _priority = priority;
            _changeFrequently = changeFrequently;
        }

        #endregion Constructeur.

        #region Énumérations.

        /// <summary>
        ///     Fréquence probable de modification de la page.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Frequence")]
        public enum Frequence
        {
            /// <summary>
            ///     Always.
            /// </summary>
            Always,

            /// <summary>
            ///     Hourly.
            /// </summary>
            Hourly,

            /// <summary>
            ///     Daily.
            /// </summary>
            Daily,

            /// <summary>
            ///     Weekly.
            /// </summary>
            Weekly,

            /// <summary>
            ///     Monthly.
            /// </summary>
            Monthly,

            /// <summary>
            ///     AnnuelleYearly.
            /// </summary>
            Yearly,

            /// <summary>
            ///     Never.
            /// </summary>
            Never
        }

        #endregion Énumérations.

        #region Méthodes.

        /// <summary>
        ///     Determines if the date is W3C format.
        /// </summary>
        /// <param name="lastModification">Date of last modification file.</param>
        /// <exception cref="FormatException">Throw if <paramref name="lastModification" /> isn't in the W3C format.</exception>
        private static bool IsValidDate(string lastModification)
        {
            var supportedFormats = new[] { "yyyy-MM-dd", "YYYY-MM-DDThh:mmTZD", "YYYY-MM-DDThh:mm:ssTZD", "YYYY-MM-DDThh:mm:ss.sTZD" };

            DateTime result;
            return DateTime.TryParseExact(lastModification, supportedFormats, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out result);
        }

        #endregion Méthodes.
    }
}