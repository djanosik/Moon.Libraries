using System;

namespace Moon
{
    /// <summary>
    /// Provides helpers for dealing with time.
    /// </summary>
    public static class Time
    {
        /// <summary>
        /// Gets the Coordinated Universal Time (UTC) zone.
        /// </summary>
        public static TimeZone UtcZone { get; } = new UtcTimeZone();

        /// <summary>
        /// Converts a Coordinated Universal Time (UTC) to the time in a specified time zone.
        /// </summary>
        /// <param name="dateTime">The Coordinated Universal Time (UTC).</param>
        /// <param name="destTimeZone">
        /// The time zone to convert the <paramref name="dateTime" /> to.
        /// </param>
        /// <returns>
        /// The date and time in the destination time zone. Its <see cref="DateTime.Kind" />
        /// property is <see cref="DateTimeKind.Utc" /> if destinationTimeZone is UTC; otherwise,
        /// its <see cref="DateTime.Kind" /> property is <see cref="DateTimeKind.Unspecified" />.
        /// </returns>
        public static DateTime? FromUtc(DateTime? dateTime, TimeZone destTimeZone)
            => dateTime == null ? (DateTime?)null : FromUtc(dateTime.Value, destTimeZone);

        /// <summary>
        /// Converts a Coordinated Universal Time (UTC) to the time in a specified time zone.
        /// </summary>
        /// <param name="dateTime">The Coordinated Universal Time (UTC).</param>
        /// <param name="destTimeZone">
        /// The time zone to convert the <paramref name="dateTime" /> to.
        /// </param>
        /// <returns>
        /// The date and time in the destination time zone. Its <see cref="DateTime.Kind" />
        /// property is <see cref="DateTimeKind.Utc" /> if destinationTimeZone is UTC; otherwise,
        /// its <see cref="DateTime.Kind" /> property is <see cref="DateTimeKind.Unspecified" />.
        /// </returns>
        public static DateTime FromUtc(DateTime dateTime, TimeZone destTimeZone)
        {
            Requires.That(dateTime.Kind != DateTimeKind.Local, "dateTime is local date time");

            return UtcZone.ConvertTime(dateTime, destTimeZone);
        }

        /// <summary>
        /// Converts the time in a specified time zone to Coordinated Universal Time (UTC).
        /// </summary>
        /// <param name="dateTime">The Coordinated Universal Time (UTC).</param>
        /// <param name="srcTimeZone">The time zone of the <paramref name="dateTime" />.</param>
        /// <returns>
        /// The Coordinated Universal Time (UTC) that corresponds to the dateTime parameter. Its
        /// <see cref="DateTime.Kind" /> property is <see cref="DateTimeKind.Utc" />.
        /// </returns>
        public static DateTime? ToUtc(DateTime? dateTime, TimeZone srcTimeZone)
            => dateTime == null ? (DateTime?)null : ToUtc(dateTime.Value, srcTimeZone);

        /// <summary>
        /// Converts the time in a specified time zone to Coordinated Universal Time (UTC).
        /// </summary>
        /// <param name="dateTime">The Coordinated Universal Time (UTC).</param>
        /// <param name="srcTimeZone">The time zone of the <paramref name="dateTime" />.</param>
        /// <returns>
        /// The Coordinated Universal Time (UTC) that corresponds to the dateTime parameter. Its
        /// <see cref="DateTime.Kind" /> property is <see cref="DateTimeKind.Utc" />.
        /// </returns>
        public static DateTime ToUtc(DateTime dateTime, TimeZone srcTimeZone)
        {
            if (dateTime.Kind != DateTimeKind.Utc)
            {
                dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
                return srcTimeZone.ConvertTime(dateTime, UtcZone);
            }

            return dateTime;
        }
    }
}