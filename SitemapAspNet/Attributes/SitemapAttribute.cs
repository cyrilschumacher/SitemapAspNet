﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.Mvc;

namespace SitemapAspNet.Attributes
{
    /// <summary>
    ///     Attribute for <see cref="ActionResult" /> method representing a Web resource (HTML files, JPEG files, etc.).
    /// </summary>
    [CLSCompliant(true), AttributeUsage(AttributeTargets.Method, Inherited = false)]
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Sitemap"),
     SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    public sealed class SitemapAttribute : Attribute
    {
        #region Constants section.

        /// <summary>
        ///     Default priority of a page.
        /// </summary>
        public const double PriorityDefault = 0.5;

        #endregion Constants section.

        #region Members section.

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

        #endregion Members section.

        #region Properties section.

        /// <summary>
        ///     Get or set a URL entry.
        /// </summary>
        /// <value>URL entry.</value>
        /// <remarks>This property is required and is used to resolve the URL.</remarks>
        public string Address { get; set; }

        /// <summary>
        ///     Get a frequency of the page.
        /// </summary>
        /// <value>Frequency.</value>
        /// <seealso cref="Frequence">This property is optional and represents the rate of change of the page.</seealso>
        public string ChangeFrequently
        {
            get { return _changeFrequently; }
        }

        /// <summary>
        ///     Get a date of last modification file.
        /// </summary>
        /// <value>Date of last modification file.</value>
        /// <remarks>This property is optional and the date must be in the format W3C: YYYY-MM-DDThh:mmTZD.</remarks>
        public string LastModification
        {
            get { return _lastModification; }
        }

        /// <summary>
        ///     Get a priority of URL entry.
        /// </summary>
        /// <value>Priority of URL entry.</value>
        /// <remarks>The property is optional and must be represented by float number between 0.0 and 1.0.</remarks>
        public string Priority
        {
            get { return _priority; }
        }

        #endregion Properties section.

        #region Constructors section.

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <seealso cref="SitemapAttribute(string, Frequence, double)"/>
        public SitemapAttribute()
            : this(null, Frequence.Never, PriorityDefault)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="lastModification">Date of last modification file.</param>
        /// <seealso cref="SitemapAttribute(string, Frequence, double)"/>
        public SitemapAttribute(string lastModification)
            : this(lastModification, Frequence.Never, PriorityDefault)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="lastModification">Date of last modification file.</param>
        /// <param name="changeFrequently">Frequency of the page.</param>
        /// <seealso cref="SitemapAttribute(string, Frequence, double)"/>
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
        /// <exception cref="ArgumentException">Throw if <paramref name="lastModification" /> isn't in W3C format.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Throw if <paramref name="priority" /> has a value less than 0.0 or
        ///     greater than 1.0.
        /// </exception>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public SitemapAttribute(string lastModification, Frequence changeFrequently, double priority)
        {
            if (!_IsValidPriority(priority))
            {
                throw new ArgumentOutOfRangeException("priority", priority, "Priority must be between 0.0 and 1.0.");
            }

            if ((lastModification != null) && !_IsValidDate(lastModification))
            {
                throw new ArgumentException("The date isn't in the W3C format.", "lastModification");
            }

            if (!changeFrequently.Equals(Frequence.Never))
            {
                _changeFrequently = changeFrequently.ToString().ToLowerInvariant();
            }

            if (!priority.Equals(0.5))
            {
                _priority = priority.ToString(CultureInfo.InvariantCulture);
            }

            _lastModification = lastModification;
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

        #endregion Constructors section.

        #region Enumerations section.

        /// <summary>
        ///     Frequency of the page.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Frequence")]
        public enum Frequence
        {
            /// <summary>
            ///     Always.
            /// </summary>
            /// <remarks>Use for pages that change every time they are accessed.</remarks>
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
            /// <remarks>Use this value for archived URLs.</remarks>
            Never
        }

        #endregion Enumerations section.

        #region Methods section.

        /// <summary>
        ///     Determines if the date is W3C format.
        /// </summary>
        /// <param name="lastModification">Date of last modification file.</param>
        /// <returns>True if the date is valid, False else.</returns>
        private static bool _IsValidDate(string lastModification)
        {
            var supportedFormats = new[]
            {"yyyy-MM-dd", "YYYY-MM-DDThh:mmTZD", "YYYY-MM-DDThh:mm:ssTZD", "YYYY-MM-DDThh:mm:ss.sTZD"};

            DateTime result;
            return DateTime.TryParseExact(lastModification, supportedFormats, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out result);
        }

        /// <summary>
        ///     Determines if the priority is between 0.0 and 1.0.
        /// </summary>
        /// <param name="priority">Priority.</param>
        /// <returns>True if the priority is valid, False else.</returns>
        /// <returns></returns>
        private static bool _IsValidPriority(double priority)
        {
            return (0.0 <= priority) && (priority <= 1.0);
        }

        #endregion Methods section.
    }
}