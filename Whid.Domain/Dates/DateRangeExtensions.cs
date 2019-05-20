using System;

namespace Whid.Domain.Dates
{
    public static class DateRangeExtensions
    {

        /// <summary>
        /// Check if a date falls within the DateRange.
        /// The date must start on or after the start day and end on or before the end day. Time of day is ignored.
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns>True if the date is within the DateRange, false otherwise.</returns>
        public static bool IncludesDate(this DateRange dateRange, DateTime date) =>
            dateRange.StartTime <= date && date < dateRange.EndTime.AddDays(1);

        /// <summary>
        /// Check if another date range falls within the DateRange.
        /// The start date and the end date of the specified date range must both fall within the current DateRange.
        /// </summary>
        /// <param name="dateRange">The date range to check.</param>
        /// <returns>True if the specified date range is within the DateRange, false otherwise.</returns>
        public static bool IncludesDateRange(this DateRange dateRange, DateRange other) =>
            dateRange.IncludesDate(other.StartTime) && dateRange.IncludesDate(other.EndTime);

        /// <summary>
        /// Check if another date range falls at least partially within the DateRange.
        /// The specified date range has to be no longer than the current DateRange, and
        /// either start date or end date of the specified date range (or both) must fall within the current DateRange.
        /// </summary>
        /// <param name="dateRange">The date range to check.</param>
        /// <returns>True if the specified date range is at least partially within the DateRange, false otherwise.</returns>
        public static bool PartiallyIncludesDateRange(this DateRange dateRange, DateRange other) =>
            other.GetDuration() <= dateRange.GetDuration() && (dateRange.IncludesDate(other.StartTime) || dateRange.IncludesDate(other.EndTime));
    }
}
