using System;
using System.Collections.Generic;

namespace Whid.Domain.Dates
{
    /// <summary>
    /// A date range that starts on a certain day and ends on a day on or after the starting day.
    /// </summary>
    public sealed class DateRange : IEquatable<DateRange>
    {
        /// <summary>
        /// The starting day of the date range.
        /// </summary>
        public DateTime StartTime { get; }

        /// <summary>
        /// The last day of the date range.
        /// </summary>
        public DateTime EndTime { get; }

        /// <summary>
        /// The length of the date range in number of days.
        /// </summary>
        public TimeSpan GetDuration() => TimeSpan.FromDays(EndTime.Subtract(StartTime).TotalDays + 1);

        /// <summary>
        /// Construct a new DateRange.
        /// </summary>
        /// <param name="startingDay">The first day of the range.</param>
        /// <param name="inclusiveEndDay">The last day of the range.</param>
        public DateRange(Date startingDay, Date inclusiveEndDay)
        {
            StartTime = startingDay;
            EndTime = inclusiveEndDay;

            if (EndTime < StartTime)
            {
                throw new ArgumentOutOfRangeException(nameof(inclusiveEndDay), inclusiveEndDay, $"End date cannot be earlier than start date.");
            }
        }

        #region Equality and Hashcode methods
        public override bool Equals(object obj) => Equals(obj as DateRange);
        public bool Equals(DateRange other) => other != null && StartTime == other.StartTime && EndTime == other.EndTime;
        public static bool operator ==(DateRange left, DateRange right) => EqualityComparer<DateRange>.Default.Equals(left, right);
        public static bool operator !=(DateRange left, DateRange right) => !(left == right);
        public override int GetHashCode()
        {
            var hashCode = -445957783;
            hashCode = hashCode * -1521134295 + StartTime.GetHashCode();
            hashCode = hashCode * -1521134295 + EndTime.GetHashCode();
            return hashCode;
        }
        #endregion
    }
}
