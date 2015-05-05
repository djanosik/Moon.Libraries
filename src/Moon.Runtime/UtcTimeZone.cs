using System;

namespace Moon
{
    /// <summary>
    /// The Coordinated Universal Time (UTC) zone.
    /// </summary>
    public sealed class UtcTimeZone : TimeZone
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UtcTimeZone" /> class.
        /// </summary>
        public UtcTimeZone()
            : base("Etc/GMT")
        {
        }

        /// <summary>
        /// Converts a Coordinated Universal Time (UTC) to the time in a specified time zone.
        /// </summary>
        /// <param name="dateTime">The date and time to convert.</param>
        /// <param name="destTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
        /// <returns>
        /// The date and time in the destination time zone. It's <see cref="DateTime.Kind" />
        /// property is <see cref="DateTimeKind.Utc" /> if destinationTimeZone is UTC; otherwise,
        /// it's <see cref="DateTime.Kind" /> property is <see cref="DateTimeKind.Unspecified" />.
        /// </returns>
        public override DateTime ConvertTime(DateTime dateTime, TimeZone destTimeZone)
        {
            Requires.That(dateTime.Kind != DateTimeKind.Local, "dateTime is local date time");

            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

            if (destTimeZone != this)
            {
                return TimeZoneInfo.ConvertTime(dateTime, destTimeZone.Info);
            }

            return dateTime;
        }

        /// <summary>
        /// Retrieves the <see cref="TimeZoneInfo.Utc" /> time zone.
        /// </summary>
        protected override TimeZoneInfo GetTimeZoneInfo()
            => TimeZoneInfo.Utc;
    }
}