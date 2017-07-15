using System;

namespace Moon
{
    /// <summary>
    /// The base class for time zones.
    /// </summary>
    public abstract class TimeZoneBase : IEquatable<TimeZoneBase>, IEquatable<TimeZoneInfo>
    {
        private readonly Lazy<TimeZoneInfo> info;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeZoneBase" /> class.
        /// </summary>
        /// <param name="id">The IANA Time Zone identifier.</param>
        protected TimeZoneBase(string id)
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
        public TimeZoneInfo Info => info.Value;

        /// <summary>
        /// Returns whether the specified time zone is equal to the current one.
        /// </summary>
        /// <param name="other">The other time zone.</param>
        public bool Equals(TimeZoneBase other)
        {
            return Info.Equals(other.Info);
        }

        /// <summary>
        /// Returns whether the specified time zone is equal to the current one.
        /// </summary>
        /// <param name="other">The other time zone.</param>
        public bool Equals(TimeZoneInfo other)
        {
            return Info.Equals(other);
        }

        /// <summary>
        /// Converts a time from the current time zone to another.
        /// </summary>
        /// <param name="dateTime">The date and time to convert.</param>
        /// <param name="destTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
        /// <returns>
        /// The date and time in the destination time zone. Its <see cref="DateTime.Kind" />
        /// is <see cref="DateTimeKind.Utc" /> if <paramref name="destTimeZone"/> is UTC; otherwise,
        /// it's <see cref="DateTime.Kind" /> is <see cref="DateTimeKind.Unspecified" />.
        /// </returns>
        public abstract DateTime ConvertTime(DateTime dateTime, TimeZoneBase destTimeZone);

        /// <summary>
        /// Returns whether the specified time zone is equal to the current one.
        /// </summary>
        /// <param name="obj">The other time zone.</param>
        public override bool Equals(object obj)
        {
            if (obj is TimeZoneBase timeZone)
            {
                return Equals(timeZone);
            }

            if (obj is TimeZoneInfo timeZoneInfo)
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