using System;

namespace Moon
{
    /// <summary>
    /// The <see cref="IDateProvider" /> implementation based on the <see cref="DateTime" />.
    /// </summary>
    public sealed class SystemDateProvider : IDateProvider
    {
        /// <summary>
        /// Gets a <see cref="DateTime" /> that is set to the current date and time on this
        /// computer, expressed as the local time.
        /// </summary>
        public DateTime Now => DateTime.Now;

        /// <summary>
        /// Gets a <see cref="DateTime" /> that is set to the current date and time on this
        /// computer, expressed as the UTC.
        /// </summary>
        public DateTime UtcNow => DateTime.UtcNow;

        /// <summary>
        /// Gets a <see cref="DateTime" /> that is set to the current date.
        /// </summary>
        public DateTime Today => DateTime.Today;
    }
}