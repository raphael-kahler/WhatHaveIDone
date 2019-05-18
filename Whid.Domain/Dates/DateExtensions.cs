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


        public static DateRange SingleDayRange(this Date date) =>
            new DateRange(date, date);
    }
}
