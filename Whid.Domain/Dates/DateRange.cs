using System;
using System.Collections.Generic;

namespace Whid.Domain.Dates
{
    /// <summary>
    /// A date range that starts on a certain day and ends on a day on or after the starting day.
    /// </summary>
    public sealed class DateRange : IDateRange, IEquatable<DateRange>
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

        /// <summary>
        /// Check if a date falls within the DateRange.
        /// The date must start on or after the start day and end on or before the end day. Time of day is ignored.
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns>True if the date is within the DateRange, false otherwise.</returns>
        public bool IncludesDate(DateTime date) =>
            StartTime <= date && date < EndTime.AddDays(1);

        /// <summary>
        /// Check if another date range falls within the DateRange.
        /// The start date and the end date of the specified date range must both fall within the current DateRange.
        /// </summary>
        /// <param name="dateRange">The date range to check.</param>
        /// <returns>True if the specified date range is within the DateRange, false otherwise.</returns>
        public bool IncludesDateRange(IDateRange dateRange) =>
            IncludesDate(dateRange.StartTime) && IncludesDate(dateRange.EndTime);

        /// <summary>
        /// Check if another date range falls at least partially within the DateRange.
        /// The specified date range has to be no longer than the current DateRange, and
        /// either start date or end date of the specified date range (or both) must fall within the current DateRange.
        /// </summary>
        /// <param name="dateRange">The date range to check.</param>
        /// <returns>True if the specified date range is at least partially within the DateRange, false otherwise.</returns>
        public bool PartiallyIncludesDateRange(IDateRange dateRange) =>
            dateRange.GetDuration() <= GetDuration() && (IncludesDate(dateRange.StartTime) || IncludesDate(dateRange.EndTime));

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
