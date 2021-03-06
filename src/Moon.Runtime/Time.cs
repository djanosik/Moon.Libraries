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
        public static TimeZoneBase UtcZone { get; } = new UtcTimeZone();

        /// <summary>
        /// Converts a Coordinated Universal Time (UTC) to the time in a specified time zone.
        /// </summary>
        /// <param name="dateTime">The Coordinated Universal Time (UTC).</param>
        /// <param name="destTimeZone">
        /// The time zone to convert the <paramref name="dateTime" /> to.
        /// </param>
        /// <returns>
        /// The date and time in the destination time zone. Its <see cref="DateTime.Kind" />
        /// is <see cref="DateTimeKind.Utc" /> if <paramref name="destTimeZone"/> is UTC; otherwise,
        /// it's <see cref="DateTime.Kind" /> is <see cref="DateTimeKind.Unspecified" />.
        /// </returns>
        public static DateTime? FromUtc(DateTime? dateTime, TimeZoneBase destTimeZone)
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
        /// is <see cref="DateTimeKind.Utc" /> if <paramref name="destTimeZone"/> is UTC; otherwise,
        /// it's <see cref="DateTime.Kind" /> is <see cref="DateTimeKind.Unspecified" />.
        /// </returns>
        public static DateTime FromUtc(DateTime dateTime, TimeZoneBase destTimeZone)
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
        /// The Coordinated Universal Time (UTC) that corresponds to the <paramref name="dateTime"/> parameter. 
        /// Its <see cref="DateTime.Kind" /> is <see cref="DateTimeKind.Utc" />.
        /// </returns>
        public static DateTime? ToUtc(DateTime? dateTime, TimeZoneBase srcTimeZone)
            => dateTime == null ? (DateTime?)null : ToUtc(dateTime.Value, srcTimeZone);

        /// <summary>
        /// Converts the time in a specified time zone to Coordinated Universal Time (UTC).
        /// </summary>
        /// <param name="dateTime">The Coordinated Universal Time (UTC).</param>
        /// <param name="srcTimeZone">The time zone of the <paramref name="dateTime" />.</param>
        /// <returns>
        /// The Coordinated Universal Time (UTC) that corresponds to the dateTime parameter. 
        /// Its <see cref="DateTime.Kind" /> is <see cref="DateTimeKind.Utc" />.
        /// </returns>
        public static DateTime ToUtc(DateTime dateTime, TimeZoneBase srcTimeZone)
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