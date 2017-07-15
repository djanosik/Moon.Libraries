using System;

namespace Moon
{
    /// <summary>
    /// The Coordinated Universal Time (UTC) zone.
    /// </summary>
    public sealed class UtcTimeZone : TimeZoneBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UtcTimeZone" /> class.
        /// </summary>
        public UtcTimeZone()
            : base("Etc/GMT")
        {
        }

        /// <inheritdoc />
        public override DateTime ConvertTime(DateTime dateTime, TimeZoneBase destTimeZone)
        {
            Requires.That(dateTime.Kind != DateTimeKind.Local, "dateTime is local date time");

            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

            if (destTimeZone != this)
            {
                return TimeZoneInfo.ConvertTime(dateTime, destTimeZone.Info);
            }

            return dateTime;
        }

        /// <inheritdoc />
        protected override TimeZoneInfo GetTimeZoneInfo()
            => TimeZoneInfo.Utc;
    }
}