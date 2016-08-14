using System;

namespace Moon
{
    /// <summary>
    /// The base class for time zones.
    /// </summary>
    public abstract class TimeZone : IEquatable<TimeZone>, IEquatable<TimeZoneInfo>
    {
        private readonly Lazy<TimeZoneInfo> info;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeZone" /> class.
        /// </summary>
        /// <param name="id">The IANA Time Zone identifier.</param>
        protected TimeZone(string id)
        {
            Id = id;
            info = Lazy.From(GetTimeZoneInfo);
        }

        /// <summary>
        /// Gets the IANA Time Zone identifier.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the <see cref="TimeZoneInfo" /> representing the time zone.
        /// </summary>
        public TimeZoneInfo Info
            => info.Value;

        /// <summary>
        /// Returns whether the specified time zone is equal to the current one.
        /// </summary>
        /// <param name="other">The other time zone.</param>
        public bool Equals(TimeZone other)
            => Info.Equals(other.Info);

        /// <summary>
        /// Returns whether the specified time zone is equal to the current one.
        /// </summary>
        /// <param name="other">The other time zone.</param>
        public bool Equals(TimeZoneInfo other)
            => Info.Equals(other);

        /// <summary>
        /// Converts a time from the current time zone to another.
        /// </summary>
        /// <param name="dateTime">The date and time to convert.</param>
        /// <param name="destTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
        /// <returns>
        /// The date and time in the destination time zone. It's <see cref="DateTime.Kind" />
        /// property is <see cref="DateTimeKind.Utc" /> if destinationTimeZone is UTC; otherwise,
        /// it's <see cref="DateTime.Kind" /> property is <see cref="DateTimeKind.Unspecified" />.
        /// </returns>
        public abstract DateTime ConvertTime(DateTime dateTime, TimeZone destTimeZone);

        /// <summary>
        /// Returns whether the specified time zone is equal to the current one.
        /// </summary>
        /// <param name="obj">The other time zone.</param>
        public override bool Equals(object obj)
        {
            var timeZone = obj as TimeZone;
            if (timeZone != null)
            {
                return Equals(timeZone);
            }

            var timeZoneInfo = obj as TimeZoneInfo;
            if (timeZoneInfo != null)
            {
                return Equals(timeZoneInfo);
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code for this time zone.
        /// </summary>
        public override int GetHashCode()
            => Info.GetHashCode();

        /// <summary>
        /// Retrieves the <see cref="TimeZoneInfo" /> representing the time zone.
        /// </summary>
        protected abstract TimeZoneInfo GetTimeZoneInfo();
    }
}