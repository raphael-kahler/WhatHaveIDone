﻿using System;

namespace Whid.Domain.Dates
{
    public static class DateExtensions
    {
        /// <summary>
        /// Create a DateRange whose length is the specified number of weeks.
        /// </summary>
        /// <param name="startingDay">The date to use as the start date.</param>
        /// <param name="numWeeks">The number of weeks in the DateRange.</param>
        /// <returns>The created DateRange.</returns>
        public static DateRange RangeFromWeeks(this Date startingDay, int numWeeks) =>
            startingDay.RangeFromDays(7 * numWeeks);

        /// <summary>
        /// Create a DateRange with the specified number of days starting from the current Date.
        /// A range with numDays == 1 will end on the same day that it starts.
        /// A range with numDays == 2 will end one day after it starts.
        /// </summary>
        /// <param name="date">The date to use as the start date.</param>
        /// <param name="numDays">The number of days in the DateRange.</param>
        /// <returns>The created DateRange.</returns>
        public static DateRange RangeFromDays(this Date date, int numDays) =>
            new DateRange(date, date.AddDays(numDays - 1));

        /// <summary>
        /// Create a DateRange that is one day long and ends on the same day that it starts.
        /// </summary>
        /// <param name="date">The start date, which is equal to the end date.</param>
        /// <returns>The created DateRange.</returns>
        public static DateRange SingleDayRange(this Date date) =>
            new DateRange(date, date);

        /// <summary>
        /// Return a DateTime that is the start of the week which contains the specified date time.
        /// Weeks start on Sunday and end on Saturday.
        /// </summary>
        /// <param name="dateTime">The date time that is contained in the week.</param>
        /// <returns>The start time of the week that contains the specified date time.</returns>
        public static Date FirstDayOfWeek(this Date date) =>
            date.AddDays(-(int)((DateTime)date).DayOfWeek);
    }
}
