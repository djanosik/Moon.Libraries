using System;

namespace Moon
{
    /// <summary>
    /// Defines basic contract for date / time providers.
    /// </summary>
    public interface IDateProvider
    {
        /// <summary>
        /// Gets a <see cref="DateTime" /> that is set to the current date and time on this
        /// computer, expressed as the local time.
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// Gets a <see cref="DateTime" /> that is set to the current date and time on this
        /// computer, expressed as the UTC.
        /// </summary>
        DateTime UtcNow { get; }

        /// <summary>
        /// Gets a <see cref="DateTime" /> that is set to the current date.
        /// </summary>
        DateTime Today { get; }
    }
}